using System.Linq;
using System.Web.Mvc;
using FamilyTreeApp.Data;

namespace FamilyTreeApp.Controllers
{
    public class HomeController : Controller
    {
        private FamilyTreeContext db = new FamilyTreeContext();

        public ActionResult Index()
        {
            // Lấy tin nổi bật
            ViewBag.FeaturedNews = db.News.Where(n => n.IsFeatured).OrderByDescending(n => n.CreatedDate).Take(3).ToList();
            
            // Lấy thống kê
            ViewBag.TotalMembers = db.FamilyMembers.Count();
            ViewBag.TotalNews = db.News.Count();
            ViewBag.TotalGalleryImages = db.GalleryImages.Count();

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
