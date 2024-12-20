using Microsoft.AspNetCore.Mvc;

namespace StudentsForStudents.Controllers
{
    public class AdminController : Controller
    {
        public async Task <IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> CoursesManagement ()
        {
            return View();
        }
        public async Task<IActionResult> UsersManagement()
        {
            return View();
        }
        public async Task<IActionResult> CalendarManagement()
        {
            return View();
        }
        public async Task<IActionResult> TeacherReq()
        {
            return View();
        }


    }
}
