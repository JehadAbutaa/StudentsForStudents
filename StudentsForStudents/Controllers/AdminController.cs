using Microsoft.AspNetCore.Mvc;

namespace StudentsForStudents.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
