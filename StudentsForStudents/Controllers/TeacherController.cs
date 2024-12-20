using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartLineSystem.Models;
using StudentsForStudents.Context;

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
            var UsereMAIL = HttpContext.Session.GetString("UserEmail");
            var User = await _context.Teacher.Where(x => x.Email == UsereMAIL).FirstOrDefaultAsync();
            return View(User);
            
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
    }
}
