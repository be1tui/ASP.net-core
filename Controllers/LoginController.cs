using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LMS_WEBSITE.Models;
using LMS_WEBSITE.Utilities;
using System.Net;
using System.Net.Mail;

namespace LMS_WEBSITE.Controllers
{
    public class LoginController : Controller
    {
        private readonly DataContext _context;

        public LoginController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(AdminUser user)
        {
            if (user == null) return NotFound();

            // Convert password to MD5
            string pw = Functions.Md5Password(user.Password);

            // Check the information in the database
            var check = _context.adminUsers.Where(u => u.UserName == user.UserName && u.Password == pw).FirstOrDefault();
            if (check == null)
            {
                Functions._Message = "Invalid username and password!";
                return RedirectToAction("Index", "Login");
            }

            Functions._Message = string.Empty;
            Functions._UserID = check.UserID;
            Functions._UserName = string.IsNullOrEmpty(check.UserName) ? string.Empty : check.UserName;
            Functions._Email = string.IsNullOrEmpty(check.Email) ? string.Empty : check.Email;

            // Redirect based on role
            if (check.Role == 1) // Admin
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            else if (check.Role == 0) // User
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Login");
            // return RedirectToAction("Index", "Home");
        }
    }
}