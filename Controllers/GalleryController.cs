using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FamilyTreeApp.Data;
using FamilyTreeApp.Models;

namespace FamilyTreeApp.Controllers
{
    public class GalleryController : Controller
    {
        private FamilyTreeContext db = new FamilyTreeContext();

        // GET: Gallery
        public ActionResult Index(string category = "")
        {
            var images = db.GalleryImages.AsQueryable();

            if (!string.IsNullOrEmpty(category) && category != "All")
            {
                images = images.Where(i => i.Category == category);
            }

            ViewBag.Categories = db.GalleryImages.Select(i => i.Category).Distinct().ToList();
            ViewBag.SelectedCategory = category;

            return View(images.OrderByDescending(i => i.UploadedDate).ToList());
        }

        // GET: Gallery/Upload
        [Authorize]
        public ActionResult Upload()
        {
            ViewBag.FamilyMembers = new SelectList(db.FamilyMembers, "Id", "FullName");
            ViewBag.Categories = new SelectList(new[] { "Gia đình", "Sự kiện", "Kỷ niệm" });
            return View();
        }

        // POST: Gallery/Upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Upload(GalleryImage image, HttpPostedFileBase imageFile)
        {
            if (imageFile != null && imageFile.ContentLength > 0)
            {
                try
                {
                    string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                    string extension = Path.GetExtension(imageFile.FileName);
                    fileName = fileName + "_" + DateTime.Now.Ticks + extension;
                    string path = Path.Combine(Server.MapPath("~/Uploads/Gallery"), fileName);

                    if (!Directory.Exists(Server.MapPath("~/Uploads/Gallery")))
                        Directory.CreateDirectory(Server.MapPath("~/Uploads/Gallery"));

                    imageFile.SaveAs(path);
                    image.ImagePath = fileName;
                    image.UploadedDate = DateTime.Now;

                    db.GalleryImages.Add(image);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("", "Lỗi khi upload ảnh");
                }
            }

            ViewBag.FamilyMembers = new SelectList(db.FamilyMembers, "Id", "FullName", image.FamilyMemberId);
            ViewBag.Categories = new SelectList(new[] { "Gia đình", "Sự kiện", "Kỷ niệm" });
            return View(image);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
