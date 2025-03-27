using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LMS_WEBSITE.Models;
using LMS_WEBSITE.Utilities;

namespace LMS_WEBSITE.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (!Functions.islogin()) 
            return RedirectToAction("Index", "Login");
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public IActionResult Logout()
    {
        Functions._UserID = 0;
        Functions._UserName = string.Empty;
        Functions._Email = string.Empty;
        Functions._Message = string.Empty;
        
        return RedirectToAction("Index", "Home");
    }
}
