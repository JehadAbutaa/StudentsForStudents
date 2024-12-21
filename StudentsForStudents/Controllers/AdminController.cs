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
        public async Task<IActionResult> CoursesManagement ()
        {
            var ALLCources = await _context.Courses.ToListAsync();
            return View(ALLCources);
        }
        public async Task<IActionResult> StdManagement()
        {
            var ALLSTD = await _context.Students.ToListAsync();
            return View(ALLSTD);
        }
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

        public async Task<IActionResult> CalendarManagement()
        {
            return View();
        }


        public async Task<IActionResult> TeacherReqHandel(string action, string email , DateTime date)
        {
            var courseRequest = await _context.CourseRequests
                                       .FirstOrDefaultAsync(cr => cr.RequestedBy == email && cr.CreatedAt == date);

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

        
        public async Task<IActionResult> RemoveStd()
        {
            return View();
        }


    }
}
