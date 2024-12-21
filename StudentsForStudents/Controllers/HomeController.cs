using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SmartLineSystem.Models;
using StudentsForStudents.Models;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using StudentsForStudents.Context;
using Microsoft.EntityFrameworkCore;
using MailKit.Net.Smtp;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;
using NuGet.Protocol.Plugins;

namespace StudentsForStudents.Controllers
{

    public class HomeController : Controller
    {
        private readonly string _sendGridApiKey = "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";
        private const string GoogleAppPassword = "rqnj bhkh yici baty";
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly SFSDBContect _context;
        private readonly IStringLocalizer<HomeController> _Localizer;

        public HomeController(
            SFSDBContect context,
            IStringLocalizer<HomeController> localizer,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _Localizer = localizer;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            var ALLCource = await _context.Courses.ToListAsync();
            return View(ALLCource);
        }

        [HttpPost]
        public async Task<IActionResult> Register(string FirstName, string LastName, string Email, string StudentID, string PhoneNumber, string Password, string ConfirmPass, string UserType, string Desc, string QualificationCourses, string Major, int Level, float GPA, int LevelFT, string MajorFT)
        {
            if (Password != ConfirmPass)
            {
                TempData["errorRegister"] = "Password and Confirm Password dose not match.";
                return RedirectToAction("Register");
            }
            if (Password.Length < 8 || !Password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                TempData["errorRegister"] = "Password must be at least 8 characters long and contain at least one special character.";
                return RedirectToAction("Register");
            }

            // Validate input data
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(Email) ||
                    string.IsNullOrEmpty(PhoneNumber) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPass) ||
                     (Password != ConfirmPass))
            {
                TempData["errorRegister"] = "All filed are required data.";
                return RedirectToAction("Register"); // Corrected to RedirectToAction("Register")
            }
            var existingUser = await _userManager.FindByEmailAsync(Email);
            if (existingUser != null)
            {
                TempData["errorRegister"] = "The provided email address is already registered.";
                return RedirectToAction("Register");
            }

            existingUser = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == PhoneNumber);
            if (existingUser != null)
            {
                TempData["errorRegister"] = "The provided phone number is already registered.";
                return RedirectToAction("Register");
            }

            var newUser = new ApplicationUser
            {
                UserName = Email,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
                EmailConfirmationToken = Guid.NewGuid().ToString(),
                UserType = UserType
            };

            var result = await _userManager.CreateAsync(newUser, Password);
            // await _context.SaveChangesAsync();
            if (result.Succeeded)
            {
                if (UserType == "Teacher")
                {
                    var newTeacher = new Teacher();
                    newTeacher.Email = Email;
                    newTeacher.FirstName = FirstName;
                    newTeacher.LastName = LastName;
                    newTeacher.PhoneNumber = PhoneNumber;
                    newTeacher.Major = MajorFT;
                    newTeacher.Desc = Desc;
                    newTeacher.StudentId = StudentID;
                    newTeacher.QualificationCourses = QualificationCourses;
                    newTeacher.GPA = GPA;
                    newTeacher.Level = LevelFT;

                    await _context.Teacher.AddAsync(newTeacher);
                    _context.SaveChanges();
                }
                else if (UserType == "Student")
                {
                    var newStudent = new Student();
                    newStudent.Email = Email;
                    newStudent.FirstName = FirstName;
                    newStudent.LastName = LastName;
                    newStudent.PhoneNumber = PhoneNumber;
                    newStudent.StudentId = StudentID;
                    newStudent.Major = Major;
                    newStudent.Level = Level;
                
                    await _context.Students.AddAsync(newStudent);
                    _context.SaveChanges();
                

                
                
                }
                // Generate confirmation token
                var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

                // Send confirmation email
                var callbackUrl = Url.Action(
                    "ConfirmEmail",
                    "Home",
                    new { userId = newUser.Id },
                    protocol: HttpContext.Request.Scheme);

                await SendConfirmationEmail(newUser.Id, "Confirm Your Account", HtmlEncoder.Default.Encode(callbackUrl), confirmationToken);

                TempData["messageLogin"] = "Registration successful. Please check your email for confirmation instructions.";
                return RedirectToAction("Login");
    
               
            }

                foreach (var error in result.Errors)
                {
                    TempData["errorRegister"] = error.Description;
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View();
            }
 

        [HttpPost]
        public async Task<IActionResult> Login(string Email, string Password)
            {
                if (Email == null || Password == null)
                {
                    TempData["errorLogin"] = "All filed are required.";
                    return RedirectToAction("Login");
                }
                if (!IsValidEmail(Email) && !IsValidPhoneNumber(Email))
                {
                    TempData["errorLogin"] = "Invalid email or phone number format.";
                    return RedirectToAction("Login");
                }

                var userToLogin = await _userManager.FindByEmailAsync(Email) ?? await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == Email);
                if (userToLogin == null || !(await _userManager.CheckPasswordAsync(userToLogin, Password)))
                {
                    TempData["errorLogin"] = "Email/PhoneNumber or Password is Invalid.";
                    return RedirectToAction("Login");
                }

                if (!userToLogin.EmailConfirmed)
                {
                    TempData["errorLogin"] = "You must confirm your email address before logging in.";
                    return RedirectToAction("Login");
                }

                userToLogin.IsLogedIn = true;
                await _context.SaveChangesAsync();

                HttpContext.Session.SetString("UserEmail", Email);
                HttpContext.Session.SetString("IsLoggedIn", "true");
                HttpContext.Session.SetString("UserId", userToLogin.Id.ToString());
                HttpContext.Session.SetInt32("SessionTimeout", 3600);

                // Start a timer to expire the session after 1 hour
                var sessionTimer = new Timer(state =>
                {
                    // Invalidate session and set IsLoggedIn to false
                    HttpContext.Session.Clear();
                    userToLogin.IsLogedIn = false;
                    _context.SaveChanges(); // Save changes to database

                }, null, TimeSpan.FromSeconds(3600), Timeout.InfiniteTimeSpan);


            //        if (userToLogin.UserType == "Admin")
            //        {
            //            //HttpContext.Session.Set("IsAdmin", true);
            //            var claims = new List<Claim>
            //{
            //    new Claim("UserType", "Admin")
            //    // Add other claims as needed
            //};
            //
            //            var claimsIdentity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme); // Use the correct scheme
            //            var authProperties = new AuthenticationProperties
            //            {
            //                // Configure additional authentication properties if needed
            //            };
            //
            //            await HttpContext.SignInAsync(
            //                IdentityConstants.ApplicationScheme,
            //                new ClaimsPrincipal(claimsIdentity),
            //                authProperties);
            //
            //            return RedirectToAction("Index", "Home");
            //        }
            //        if (userToLogin.UserType != "Admin")
            //        {
            //            TempData["errorLogin"] = "You must be Admin";
            //
            //        }

            if (userToLogin.UserType == "Teacher")
                return RedirectToAction("Index", "Teacher");

            return RedirectToAction("Index", "Home");



        }
        private async Task SendConfirmationEmail(string userId, string subject, string htmlMessage, string confirmationToken)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var email = user.Email;
                var firstName = user.FirstName; // Assuming you have a FirstName property in your ApplicationUser

                try
                {
                    using (var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587))
                    {
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential("studentforstudentteam@gmail.com", "tarh ynmj ttnv orwj");

                        var mailMessage = new MailMessage
                        {
                            From = new MailAddress("studentforstudentteam@gmail.com", "Student for Student Team"),
                            Subject = "Contact Form Web",
                            IsBodyHtml = true,
                            Body = GenerateHtmlMessage(firstName, htmlMessage) // Generate HTML with personalized greeting and image
                        };

                        mailMessage.To.Add(email);
                        await smtpClient.SendMailAsync(mailMessage);
                        TempData["success"] = "Message sent successfully.";

                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = $"Error sending email: {ex.Message}";
                }
            }

        }

        private string GenerateHtmlMessage(string firstName, string callbackUrl)
            {
                // HTML template for the email message
                var htmlMessage = $@"
        <!DOCTYPE html>
        <html lang=""en"">
        <head>
            <meta charset=""UTF-8"">
            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
            <title>Email Confirmation</title>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    background-color: #f5f5f5;
                    padding: 20px;
                }}
                .container {{
                    max-width: 600px;
                    margin: 0 auto;
                    background-color: #ffffff;
                    padding: 30px;
                    border-radius: 5px;
                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                }}
                .logo-img {{
                    display: block;
                    margin: 0 auto;
                    max-width: 200px;
                    height: auto;
                }}
                .message {{
                    text-align: center;
                    margin-top: 20px;
                }}
                .button {{
                    display: inline-block;
                    padding: 10px 20px;
                    background-color: #007bff;
                    color: #ffffff;
                    text-decoration: none;
                    border-radius: 5px;
                }}
            </style>
        </head>
        <body>
            <div class=""container"">
                
                <h2>Hi {firstName},</h2>
                <p>Thank you for registering with us. Please confirm your account by clicking the button below:</p>
                <p class=""message""><a href=""{HtmlEncoder.Default.Encode(callbackUrl)}"" class=""button"">Confirm Your Account</a></p>
                <p>If you did not create an account, please ignore this email.</p>
            </div>
        </body>
        </html>";

                return htmlMessage;
            }


            public async Task<IActionResult> ConfirmEmail(string userId)
            {
                // if (userId == null || code == null)
                // {
                //     TempData["error"] = "Invalid confirmation link.";
                //     return RedirectToAction("Login");
                // }
                var user = await _userManager.FindByIdAsync(userId);

                var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                if (user == null)
                {
                    TempData["error"] = "Invalid confirmation link.";
                    return RedirectToAction("Login");
                }

                var result = await _userManager.ConfirmEmailAsync(user, confirmationToken);
                if (result.Succeeded)
                {
                    user.EmailConfirmed = true;
                    await _context.SaveChangesAsync();
                    await _context.AddRangeAsync();
                    TempData["successLogin"] = "Email confirmed successfully. You can now log in.";

                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["errorLogin"] = "Error confirming your email.";
                    return RedirectToAction("Login");
                }
            }


        [HttpGet]
        public async Task<IActionResult> SendContactForm(string Name, string Email, string Message)
            {
             return View();
            }

        [HttpPost]
        public async Task<IActionResult> SendContactFormm(string Name, string Email, string Message)
            {
                var body = $"Name: {Name}\nEmail: {Email}\nMessage: {Message}";
                if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email) ||string.IsNullOrEmpty(Message))
                {
                    TempData["error"] = "All fields are required.";
                    return RedirectToAction("Contact");
                }

                try
                {
                  using (var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential("studentforstudentteam@gmail.com", "tarh ynmj ttnv orwj");

                        // Create a MailMessage object
                        var mailMessage = new MailMessage
                        {
                            From = new MailAddress("studentforstudentteam@gmail.com", "Student for Student Team"), 
                            Subject = "Contact Form Web",
                            Body = body,
                            IsBodyHtml = false // Set to true if the body contains HTML
                        };

                        // Add the recipient email address
                        mailMessage.To.Add("studentforstudentteam@gmail.com"); // Your email address

                        // Optionally, set the reply-to address to the user's email address
                        mailMessage.ReplyToList.Add(new MailAddress(Email));

                        // Send the email
                        await smtpClient.SendMailAsync(mailMessage);

                        TempData["success"] = "Message sent successfully.";
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = $"Error sending message: {ex.Message}";
                }

                return RedirectToAction("SendContactForm");
            }

        private bool IsValidEmail(string email)
            {
                if (string.IsNullOrWhiteSpace(email))
                    return false;

                try
                {
                    var addr = new MailAddress(email);
                    return addr.Address == email;
                }
                catch
                {
                    return false;
                }
            }

        private bool IsValidPhoneNumber(string phoneNumber)
            {
                return Regex.IsMatch(phoneNumber, @"^\d{1,15}$");
            }


        public IActionResult Logout()
            {
                HttpContext.Session.Clear();

                return RedirectToAction("Login");
            }

        [HttpGet]
        public IActionResult Login()
            {
                return View();
            }

        [HttpGet]
        public IActionResult Register()
            {
                return View();
            }
        public async Task SendEmailWithMailKit(string toEmail, string subject, string bodyHtml)
          {
              var message = new MimeMessage();
              message.From.Add(new MailboxAddress("StudentForStudents", "studentforstudentteam@gmail.com"));
              message.To.Add(new MailboxAddress("", toEmail));
              message.Subject = subject;
       
              var bodyBuilder = new BodyBuilder { HtmlBody = bodyHtml };
              message.Body = bodyBuilder.ToMessageBody();
       
              using var client = new MailKit.Net.Smtp.SmtpClient();
              await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
              await client.AuthenticateAsync("studentforstudentteam@gmail.com", "Jehad1023@");
              await client.SendAsync(message);
              await client.DisconnectAsync(true);
          }

        public IActionResult Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return RedirectToAction("Index");
            }

            var filteredCourses = _context.Courses
                .Where(c => c.Name.Contains(searchTerm) || c.Description.Contains(searchTerm))
                .ToList();

            return View(filteredCourses);
        }

        public async Task<IActionResult> GetAllTeacher(string courseName)
        {
            if (string.IsNullOrEmpty(courseName))
            {
                return BadRequest("Course name is required.");
            }
            

            var teachers = await _context.Teacher
                .Where(t => t.InrolmentCourses.Any(c => c.Name == courseName))
                .ToListAsync();

            return View(teachers);
        }

        public async Task<IActionResult> GetAllCources()
        {
                       
            var ALLCource = await _context.Courses.ToListAsync();
            return View(ALLCource);
        }


        public async Task<IActionResult> Profile()
        {
            var UsereMAIL = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(UsereMAIL))
            {
                return RedirectToAction("Login");
            }
            var User = await _context.Students.Where(x => x.Email == UsereMAIL).FirstOrDefaultAsync();
            if (User != null)
                return View(User);
            else
                return RedirectToAction("TeacherProfile", "Home");
        }

        public async Task<IActionResult> TeacherProfile()
        {
            var UsereMAIL = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(UsereMAIL))
            {
                return RedirectToAction("Login");
            }
            var User = await _context.Teacher.Where(x => x.Email == UsereMAIL).FirstOrDefaultAsync();
            if (User != null)
                return View(User);
            else
                RedirectToAction("Index");
            return View(User);
        }

        public async Task<IActionResult> EditProfile(string FirstName , string LastName ,string Email , string PhoneNumber , string Major ,int Level , string Desc , string StudentId , string QualificationCourses , float GPA)
        {
            var UsereMAIL = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(UsereMAIL))
            {
                return RedirectToAction("Login");
            }
            var User2 = await _context.Students.Where(x => x.Email == UsereMAIL).FirstOrDefaultAsync();
            if (User2 != null)
            {
                User2.FirstName = FirstName;
                User2.LastName = LastName;
                User2.Email = Email;
                User2.PhoneNumber = PhoneNumber;
                User2.Major = Major;
                User2.Level= Level;
                User2.StudentId = StudentId;
                _context.Update(User2);
                await _context.SaveChangesAsync();
                TempData["ProfileMSGSS"] = "Your Edit Done Successfully.";
                return RedirectToAction("Profile");


            }
            else
                TempData["ProfileMSGF"] = "SomeThing went Wrong.";



            return View(User);
        }



        public async Task<IActionResult> About()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UploadProfilePictureStudents(IFormFile ProfilePicture)
        {
            var UsereMAIL = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(UsereMAIL))
            {
                return RedirectToAction("Login");
            }


            var user = await _context.Students.Where(x => x.Email == UsereMAIL).FirstOrDefaultAsync();


            if (ProfilePicture == null || ProfilePicture.Length == 0)
            {
                ModelState.AddModelError("", "Please select a valid image.");
                return View();
            }

            using var memoryStream = new MemoryStream();
            await ProfilePicture.CopyToAsync(memoryStream);
            var imageData = memoryStream.ToArray();

            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.ProfilePicture = imageData;

            await _context.SaveChangesAsync();

            return RedirectToAction("Profile");
        }


    }
}
