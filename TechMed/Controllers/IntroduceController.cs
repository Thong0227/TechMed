using Microsoft.AspNetCore.Mvc;

namespace TechMed.Controllers
{
    public class IntroduceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
