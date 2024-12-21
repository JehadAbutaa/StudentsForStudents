using Microsoft.AspNetCore.Mvc;

namespace StudentsForStudents.Controllers
{
    public class VideoCallController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult videoCall()
        {
            return View();
        }
    }
}
