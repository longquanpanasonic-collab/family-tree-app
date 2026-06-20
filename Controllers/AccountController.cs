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
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password, bool rememberMe = false, string returnUrl = "")
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
            
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            
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
        public ActionResult Register(string username, string email, string password, string confirmPassword, string fullName)
        {
            if (string.IsNullOrEmpty(confirmPassword) || password != confirmPassword)
            {
                ModelState.AddModelError("confirmPassword", "Mật khẩu không trùng khớp");
                return View();
            }

            var existingUser = db.Users.FirstOrDefault(u => u.Username == username);
            if (existingUser != null)
            {
                ModelState.AddModelError("username", "Tên đăng nhập đã được sử dụng");
                return View();
            }

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = username,
                    Email = email,
                    Password = HashPassword(password),
                    FullName = fullName,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Role = "User"
                };

                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("Login");
            }

            return View();
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
