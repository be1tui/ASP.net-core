using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using LMS_WEBSITE.Models;
using LMS_WEBSITE.Utilities;

namespace LMS_WEBSITE.Controllers
{
    public class SendForgotPasswordEmail : Controller
    {
        private readonly DataContext _context;

        public SendForgotPasswordEmail(DataContext context)
        {
            _context = context;
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {
            var user = _context.adminUsers.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                ViewBag.Message = "Email not found!";
                return View();
            }

            // Generate reset token (for simplicity, using a GUID here)
            var resetToken = Guid.NewGuid().ToString();
            // Save the reset token to the user (you might want to save it to the database)
            user.PasswordResetToken = resetToken;
            _context.SaveChanges();

            // Send email
            var resetLink = Url.Action("ResetPassword", "SendForgotPasswordEmail", new { token = resetToken }, Request.Scheme);
            var message = new MailMessage("your-email@example.com", email)
            {
                Subject = "Password Reset",
                Body = $"Please reset your password using the following link: {resetLink}",
                IsBodyHtml = true
            };

            using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential("lenomessy1@gmail.com", "tjszbtlchceegoba");
                smtp.EnableSsl = true;
                smtp.Send(message);
            }

            ViewBag.Message = "Password reset link has been sent to your email.";
            return View();
        }

        public IActionResult ResetPassword(string token)
        {
            var user = _context.adminUsers.FirstOrDefault(u => u.PasswordResetToken == token);
            if (user == null)
            {
                ViewBag.Message = "Invalid token!";
                return View();
            }

            return View(new ResetPasswordViewModel { Token = token });
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _context.adminUsers.FirstOrDefault(u => u.PasswordResetToken == model.Token);
            if (user == null)
            {
                ViewBag.Message = "Invalid token!";
                return View();
            }

            user.Password = Functions.Md5Password(model.Password);
            user.PasswordResetToken = null; // Clear the reset token
            _context.SaveChanges();

            ViewBag.Message = "Password has been reset successfully.";
            return RedirectToAction("Index", "Login");
        }
    }
}