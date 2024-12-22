using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartLineSystem.Models;
using StudentsForStudents.Context;
using StudentsForStudents.Migrations;
using StudentsForStudents.Models;

namespace StudentsForStudents.Controllers
{
    public class AdminController : Controller
    {
        private readonly SFSDBContect _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IStringLocalizer<HomeController> _Localizer;
        public AdminController(
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





        public async Task <IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> CalendarManagement()
        {
            return View();
        }


        //  STD
        [HttpGet]
        public async Task<IActionResult> StdManagement()
        {
            var ALLSTD = await _context.Students.ToListAsync();
            return View(ALLSTD);
        }
        public async Task<IActionResult> EditStdV(int id)
        {
            var User = await _context.Students.Where(x => x.Id == id).FirstOrDefaultAsync();
            return View(User);
        }
        public async Task<IActionResult> EditStd(int id , string FirstName , string LastName , string Email, string PhoneNumber, string Major, int Level , string StudentId)
        {
            
            var User = await _context.Students.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (User!= null)
            {
                User.FirstName = FirstName;
                User.LastName = LastName;
                User.Email = Email;
                User.PhoneNumber = PhoneNumber;
                User.Major = Major;
                User.Level = Level;
                User.StudentId = StudentId;
                _context.Update(User);
                await _context.SaveChangesAsync();
                TempData["AdminEditStd"] = "Your Edit Done Successfully.";
                return RedirectToAction("StdManagement");


            }
            else
                TempData["AdminEditStd"] = "SomeThing went Wrong.";



            return View(User);
        }
        public async Task<IActionResult> DeleteStdV(int id)
        {
            var User = await _context.Students.Where(x => x.Id == id).FirstOrDefaultAsync();
            return View(User);
        }
        public async Task<IActionResult> DeleteStd(int id)
        {
            var User = await _context.Students.Where(x => x.Id == id).FirstOrDefaultAsync();
            _context.Remove(User);
            await _context.SaveChangesAsync();
            return RedirectToAction("StdManagement");
        }


        //Teacher
        public async Task<IActionResult> TeacherManagement()
        {
            var ALLTeacher= await _context.Teacher.ToListAsync();
            return View(ALLTeacher);
        }
        public async Task<IActionResult> TeacherReq()
        {
            var ALlReq = await _context.CourseRequests.ToListAsync();
            return View(ALlReq);
        }
        public async Task<IActionResult> TeacherReqHandel(string action, string email , DateTime date , string Description)
        {
            var courseRequest = await _context.CourseRequests
                                       .FirstOrDefaultAsync(cr => cr.RequestedBy == email && cr.Description == Description);

            if (courseRequest == null)
            {
                return NotFound("Course request not found for the provided email.");
            }
            if (action == "accept")
            {
                courseRequest.Status = "Accepted";
            }
            else if (action == "decline")
            {
                courseRequest.Status = "Declined";
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            return RedirectToAction("TeacherReq"); 
        }
        public async Task<IActionResult> EditTeacherV(int id)
        {
            var User = await _context.Teacher.Where(x => x.Id == id).FirstOrDefaultAsync();
            return View(User);
        }
        public async Task<IActionResult> EditTeacher(int id, string FirstName, string LastName, string Email, string PhoneNumber, string Major, int Level, string StudentId , string Desc , string QualificationCourses , float GPA , int Rate)
        {

            var Teacher = await _context.Teacher.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (User != null)
            {
                Teacher.FirstName = FirstName;
                Teacher.LastName = LastName;
                Teacher.Email = Email;
                Teacher.PhoneNumber = PhoneNumber;
                Teacher.Major = Major;
                Teacher.Level = Level;
                Teacher.StudentId = StudentId;
                Teacher.Desc = Desc;
                Teacher.QualificationCourses = QualificationCourses;
                Teacher.GPA = GPA;
                Teacher.Rate = Rate;

                _context.Update(Teacher);
                await _context.SaveChangesAsync();
                TempData["TeacherManagement"] = "Your Edit Done Successfully.";
                return RedirectToAction("TeacherManagement");


            }
            else
                TempData["TeacherManagement"] = "SomeThing went Wrong.";



            return View(Teacher);
        }
        public async Task<IActionResult> DeleteTeacherV(int id)
        {
            var Teacher = await _context.Teacher.Where(x => x.Id == id).FirstOrDefaultAsync();
            return View(Teacher);
        }
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var Teacher = await _context.Teacher.Where(x => x.Id == id).FirstOrDefaultAsync();
            _context.Remove(Teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction("TeacherManagement");
        }




        //Cources
        public async Task<IActionResult> CoursesManagement()
        {
            var ALLCources = await _context.Courses.ToListAsync();
            return View(ALLCources);
        }
        [HttpGet]
        public async Task<IActionResult> AddCource()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCource(string Name, string Description ,IFormFile CourcePicture)
        {
            var newCourse = new Courses();
             
            newCourse.Name = Name;
            newCourse.Description = Description;

            if (CourcePicture != null && CourcePicture.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await CourcePicture.CopyToAsync(memoryStream);
                    newCourse.CourcePicture = memoryStream.ToArray();
                }
            }
            // Save the course to the database
            _context.Courses.Add(newCourse);
                await _context.SaveChangesAsync();

                // Redirect to a course list or confirmation page
                return RedirectToAction("CoursesManagement");


        }
        [HttpGet]
        public async Task<IActionResult> GetEditCource(int id)
        {
            var CourcesToBeEdit = await _context.Courses.Where(x => x.Id == id).FirstOrDefaultAsync();
            return View(CourcesToBeEdit);
        }
        [HttpPost]
        public async Task<IActionResult> EditCource(int id , string Name, string Description, IFormFile CourcePicture)
        {
            var Cource = await _context.Courses.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (Cource != null)
            {
                Cource.Name = Name;
                Cource.Description = Description;
                Cource.Description = Description;

                if (CourcePicture != null && CourcePicture.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await CourcePicture.CopyToAsync(memoryStream);
                        Cource.CourcePicture = memoryStream.ToArray();
                    }
                }
                _context.Update(Cource);
                await _context.SaveChangesAsync();
                TempData["CoursesManagement"] = "Your Edit Done Successfully.";
                return RedirectToAction("CoursesManagement");


            }
            else
                TempData["CoursesManagement"] = "SomeThing went Wrong.";



            return View(User);
        }

        public async Task<IActionResult> DeleteCourceV(int id)
        {
            var Course = await _context.Courses.Where(x => x.Id == id).FirstOrDefaultAsync();
            return View(Course);
        }
        public async Task<IActionResult> DeleteCource(int id)
        {
            var Course = await _context.Courses.Where(x => x.Id == id).FirstOrDefaultAsync();
            _context.Remove(Course);
            await _context.SaveChangesAsync();
            return RedirectToAction("CoursesManagement");
        }

    }
}
