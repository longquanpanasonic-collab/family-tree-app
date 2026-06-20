using System;
using System.Web.Mvc;
using System.Web.Security;
using FamilyTreeApp.Data;
using FamilyTreeApp.Models;
using System.Linq;

namespace FamilyTreeApp.Controllers
{
    public class AccountController : Controller
    {
        private FamilyTreeContext db = new FamilyTreeContext();

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u => u.Email == model.Email);
                
                if (user != null && VerifyPassword(model.Password, user.PasswordHash) && user.IsActive)
                {
                    // Đăng nhập thành công
                    FormsAuthentication.SetAuthCookie(user.Id.ToString(), model.RememberMe);
                    user.LastLoginDate = DateTime.Now;
                    db.SaveChanges();

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/"))
                        return Redirect(returnUrl);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Email hoặc mật khẩu không đúng.");
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.Users.FirstOrDefault(u => u.Email == model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Email này đã được đăng ký.");
                    return View(model);
                }

                var user = new User
                {
                    Username = model.FullName,
                    Email = model.Email,
                    FullName = model.FullName,
                    PasswordHash = HashPassword(model.Password),
                    Role = "User",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };

                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("Login");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }

    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}