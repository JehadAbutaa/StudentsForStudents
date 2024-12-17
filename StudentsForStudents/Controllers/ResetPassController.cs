using StudentsForStudents.Context;
using StudentsForStudents.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Text.Encodings.Web;
using SmartLineSystem.Models;

namespace Kofia.Controllers
{
    public class ResetPassController : Controller
    {
       // private const string GoogleAppPassword = "kjfl vdgy lldh rgqi";
        private readonly SFSDBContect _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public ResetPassController(
           UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
           IConfiguration configuration,
           SFSDBContect kofiaDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = kofiaDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> RequestPasswordReset(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                TempData["errorResetPass"] = "Email is required.";
                return RedirectToAction("ForgotPassword");
            }

            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                TempData["errorResetPass"] = "No user found with the provided email.";
                return RedirectToAction("ForgotPassword");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "ResetPass", new { token, email = user.Email }, protocol: HttpContext.Request.Scheme);

            await SendPasswordResetEmail(user.Email, callbackUrl);

            TempData["messageResetPass"] = "Password reset email sent. Please check your email.";
            return RedirectToAction("Login", "Home");
        }

        private async Task SendPasswordResetEmail(string email, string callbackUrl)
        {
            try
            {
                using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.EnableSsl = true;
                   smtpClient.Credentials = new NetworkCredential("kofiawebteam@gmail.com", "tarh ynmj ttnv orwj");

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("studentforstudentteam@gmail.com", "Student For Student Team"),
                        Subject = "Reset Your Password",
                        IsBodyHtml = true,
                        Body = GeneratePasswordResetHtmlMessage(callbackUrl)
                    };

                    mailMessage.To.Add(email);
                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Error sending email: {ex.Message}";
            }
        }

        private string GeneratePasswordResetHtmlMessage(string callbackUrl)
        {
            var htmlMessage = $@"
    <!DOCTYPE html>
    <html lang=""en"">
    <head>
        <meta charset=""UTF-8"">
        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
        <title>Password Reset</title>
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
            <h2>Password Reset</h2>
            <p>Click the button below to reset your password:</p>
            <p><a href=""{HtmlEncoder.Default.Encode(callbackUrl)}"" class=""button"">Reset Password</a></p>
        </div>
    </body>
    </html>";

            return htmlMessage;
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                TempData["error"] = "Invalid password reset token.";
                return RedirectToAction("Login", "Home");
            }

            var model = new ResetPasswordViewModel { Token = token, Email = email };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                TempData["error"] = "No user found with the provided email.";
                return RedirectToAction("Login", "Home");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                TempData["message"] = "Password has been reset successfully.";
                return RedirectToAction("Login", "Home");
            }

            foreach (var error in result.Errors)
            {
                TempData["error"] = error.Description;
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        public async Task<IActionResult> EmailToBeChange(string Email)
        {
          return View();
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
