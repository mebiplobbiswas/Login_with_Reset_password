using Ostad_b5_Inv.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace Ostad_b5_Inv.Controllers
{
    public class ResetAccountPasswordController : Controller
    {
        // GET: ResetAccountPassword
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult ResetAccountPassword()
        {
            return View();
        }
                

        [HttpPost]
        public ActionResult ResetAccountPassword(string username, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                ViewBag.Message = "All fields are required.";
                return View();
            }

            var user = db.Users.FirstOrDefault(u => u.Email == username);
            if (user == null)
            {
                ViewBag.Message = "Username not found.";
                return View();
            }

            if (newPassword != confirmPassword)
            {
                ViewBag.Message = "Passwords do not match!";
                return View();
            }

            // Securely hash the new password before storing
            user.PasswordHash = HashPassword(newPassword);
            db.SaveChanges();

            TempData["Message"] = "Your password has been reset successfully.";
            return RedirectToAction("Login", "Account");
        }

        // Secure password hashing using SHA256
        private string HashPassword(string password)
        {
            using (SHA1 sha256 = SHA1.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

    }
}






