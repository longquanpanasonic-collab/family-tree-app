using System.Web.Mvc;
using FamilyTreeApp.Data;
using System.Linq;

namespace FamilyTreeApp.Controllers
{
    public class HomeController : Controller
    {
        private FamilyTreeContext db = new FamilyTreeContext();

        public ActionResult Index()
        {
            var featuredNews = db.News.Where(n => n.IsFeatured).OrderByDescending(n => n.CreatedDate).Take(5).ToList();
            ViewBag.FeaturedNews = featuredNews;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(string name, string email, string message)
        {
            // Gửi email hoặc lưu vào database
            TempData["Message"] = "Cảm ơn bạn đã liên hệ. Chúng tôi sẽ phản hồi sớm!";
            return RedirectToAction("Contact");
        }
    }
}