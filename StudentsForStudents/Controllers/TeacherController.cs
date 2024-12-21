using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartLineSystem.Models;
using StudentsForStudents.Context;
using StudentsForStudents.Models;

namespace StudentsForStudents.Controllers
{
    public class TeacherController : Controller
    {
        private readonly SFSDBContect _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IStringLocalizer<HomeController> _Localizer;
        public TeacherController(
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
        public async Task<IActionResult> TeacherDashbord()
        {
            var UsereMAIL = HttpContext.Session.GetString("UserEmail");
            var User = await _context.Teacher.Where(x => x.Email == UsereMAIL).FirstOrDefaultAsync();
            return View(User);

        }
        public async Task <IActionResult> AddCourses()
        {
            return View();
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

        public async Task<IActionResult> EditProfile(string FirstName, string LastName, string Email, string PhoneNumber, string Major, int Level, string Desc, string StudentId, string QualificationCourses, float GPA)
        {
            var UsereMAIL = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(UsereMAIL))
            {
                return RedirectToAction("Login");
            }
            var User = await _context.Teacher.Where(x => x.Email == UsereMAIL).FirstOrDefaultAsync();
            if (User != null)
            {
                User.FirstName = FirstName;
                User.LastName = LastName;
                User.Email = Email;
                User.PhoneNumber = PhoneNumber;
                User.Major = Major;
                User.Level = Level;
                User.Desc = Desc;
                User.StudentId = StudentId;
                User.QualificationCourses = QualificationCourses;
                User.GPA = GPA;
                _context.Update(User);
                await _context.SaveChangesAsync();
                return RedirectToAction("TeacherProfile");
            }

            return View(User);
        }

        public async Task<IActionResult> TeacherReq(string CourseName, string Description, string Faculty)
        {
            var UsereMAIL = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(UsereMAIL))
            {
                return RedirectToAction("Login");
            }
            var User = await _context.Teacher.Where(x => x.Email == UsereMAIL).FirstOrDefaultAsync();
            if (User != null)
            {
                CourseRequest courseRequest = new CourseRequest();
                courseRequest.CourceName = CourseName;
                courseRequest.Description = Description;
                courseRequest.Faculty = Faculty;
                courseRequest.RequestedBy = User.Email;
                courseRequest.Status = "Pending";
                courseRequest.CreatedAt = DateTime.Now;

                await _context.CourseRequests.AddAsync(courseRequest);
                _context.SaveChanges();
                TempData["ReqCoursesS"] = "Request Sent Successfuly.";

            }
            else
            {
                TempData["ReqCoursesF"] = "SomeThing went Wrong.";
                return RedirectToAction("AddCourses", "Teacher");

            }
            return RedirectToAction("AddCourses" , "Teacher");
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfilePictureStudents(IFormFile ProfilePicture)
        {
            var UsereMAIL = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(UsereMAIL))
            {
                return RedirectToAction("Login");
            }


            var user = await _context.Teacher.Where(x => x.Email == UsereMAIL).FirstOrDefaultAsync();


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

            return RedirectToAction("TeacherProfile");
        }
    }
}
