using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FamilyTreeApp.Data;
using FamilyTreeApp.Models;

namespace FamilyTreeApp.Controllers
{
    public class AccountController : Controller
    {
        private FamilyTreeContext db = new FamilyTreeContext();

        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password, bool rememberMe = false)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Vui lòng nhập tên đăng nhập và mật khẩu");
                return View();
            }

            var user = db.Users.FirstOrDefault(u => u.Username == username);

            if (user == null || !VerifyPassword(password, user.Password))
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                return View();
            }

            if (!user.IsActive)
            {
                ModelState.AddModelError("", "Tài khoản của bạn đã bị vô hiệu hóa");
                return View();
            }

            user.LastLogin = DateTime.Now;
            db.SaveChanges();

            // Thiết lập Forms Authentication
            FormsAuthentication.SetAuthCookie(username, rememberMe);
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user, string confirmPassword)
        {
            if (string.IsNullOrEmpty(confirmPassword) || user.Password != confirmPassword)
            {
                ModelState.AddModelError("confirmPassword", "Mật khẩu không trùng khớp");
                return View(user);
            }

            var existingUser = db.Users.FirstOrDefault(u => u.Username == user.Username);
            if (existingUser != null)
            {
                ModelState.AddModelError("Username", "Tên đăng nhập đã được sử dụng");
                return View(user);
            }

            if (ModelState.IsValid)
            {
                user.Password = HashPassword(user.Password);
                user.CreatedDate = DateTime.Now;
                user.IsActive = true;
                user.Role = "User";

                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(user);
        }

        // GET: Account/Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        // Helper methods
        private string HashPassword(string password)
        {
            return System.Web.Security.Membership.Provider.EncodePassword(password);
        }

        private bool VerifyPassword(string password, string hash)
        {
            var hashOfInput = System.Web.Security.Membership.Provider.EncodePassword(password);
            return hashOfInput == hash;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
