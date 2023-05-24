using Microsoft.AspNetCore.Mvc;

namespace GPUHunt.MVC.Controllers
{
    public class DocumentationController : Controller
    {
        public DocumentationController()
        {

        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Architecture()
        {
            return View();
        }

        public async Task<IActionResult> Technologies()
        {
            return View();
        }

        public async Task<IActionResult> Patterns()
        {
            return View();
        }

        public async Task<IActionResult> Endpoints()
        {
            return View();
        }

        public async Task<IActionResult> Services()
        {
            return View();
        }
    }
}
