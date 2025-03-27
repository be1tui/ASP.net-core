using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LMS_WEBSITE.Models;
using LMS_WEBSITE.Utilities;

namespace LMS_WEBSITE.Controllers
{
    
    public class RegisterController : Controller
    {
        private readonly DataContext _context;

        public RegisterController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(AdminUser auser)
        {
            if (auser == null) return NotFound();

            // Check the duplicate username before registering
            var check = _context.adminUsers.Where(u => u.UserName == auser.UserName).FirstOrDefault();
            if (check != null) // Already exists this username
            {
                Functions._Message = "Username already exists";
                return RedirectToAction("Index", "Register");
            }

            // If username does not exist
            Functions._Message = string.Empty;
            auser.Password = Functions.Md5Password(auser.Password);
            auser.Role = 0;
            _context.adminUsers.Add(auser);
            _context.SaveChanges();

            return RedirectToAction("Index", "Login");
        }
    }
}