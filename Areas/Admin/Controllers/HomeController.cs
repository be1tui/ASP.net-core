using Microsoft.AspNetCore.Mvc;
using LMS_WEBSITE.Utilities;

namespace LMS_WEBSITE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}