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
    public class NewsController : Controller
    {
        private FamilyTreeContext db = new FamilyTreeContext();

        // GET: News
        public ActionResult Index(string searchTitle = "", string category = "")
        {
            var news = db.News.AsQueryable();

            if (!string.IsNullOrEmpty(searchTitle))
            {
                news = news.Where(n => n.Title.Contains(searchTitle));
            }

            if (!string.IsNullOrEmpty(category) && category != "All")
            {
                news = news.Where(n => n.Category == category);
            }

            ViewBag.Categories = db.News.Select(n => n.Category).Distinct().ToList();
            ViewBag.SearchTitle = searchTitle;
            ViewBag.SelectedCategory = category;

            return View(news.OrderByDescending(n => n.CreatedDate).ToList());
        }

        // GET: News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            News news = db.News.Find(id);
            if (news == null)
                return HttpNotFound();

            news.ViewCount++;
            db.Entry(news).State = EntityState.Modified;
            db.SaveChanges();

            return View(news);
        }

        // GET: News/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(new[] { "Tin tức", "Sự kiện", "Chia sẻ" });
            return View();
        }

        // POST: News/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Title,Content,Category,IsFeatured")] News news, HttpPostedFileBase featuredImage)
        {
            if (ModelState.IsValid)
            {
                if (featuredImage != null && featuredImage.ContentLength > 0)
                {
                    try
                    {
                        string fileName = Path.GetFileNameWithoutExtension(featuredImage.FileName);
                        string extension = Path.GetExtension(featuredImage.FileName);
                        fileName = fileName + "_" + DateTime.Now.Ticks + extension;
                        string path = Path.Combine(Server.MapPath("~/Uploads/News"), fileName);

                        if (!Directory.Exists(Server.MapPath("~/Uploads/News")))
                            Directory.CreateDirectory(Server.MapPath("~/Uploads/News"));

                        featuredImage.SaveAs(path);
                        news.FeaturedImage = fileName;
                    }
                    catch { }
                }

                news.CreatedDate = DateTime.Now;
                news.Author = User.Identity.Name;
                news.ViewCount = 0;
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(new[] { "Tin tức", "Sự kiện", "Chia sẻ" });
            return View(news);
        }

        // GET: News/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            News news = db.News.Find(id);
            if (news == null)
                return HttpNotFound();

            ViewBag.Categories = new SelectList(new[] { "Tin tức", "Sự kiện", "Chia sẻ" });
            return View(news);
        }

        // POST: News/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,Category,IsFeatured,FeaturedImage,CreatedDate")] News news, HttpPostedFileBase featuredImage)
        {
            if (ModelState.IsValid)
            {
                if (featuredImage != null && featuredImage.ContentLength > 0)
                {
                    try
                    {
                        string oldPath = Path.Combine(Server.MapPath("~/Uploads/News"), news.FeaturedImage);
                        if (System.IO.File.Exists(oldPath))
                            System.IO.File.Delete(oldPath);

                        string fileName = Path.GetFileNameWithoutExtension(featuredImage.FileName);
                        string extension = Path.GetExtension(featuredImage.FileName);
                        fileName = fileName + "_" + DateTime.Now.Ticks + extension;
                        string path = Path.Combine(Server.MapPath("~/Uploads/News"), fileName);

                        featuredImage.SaveAs(path);
                        news.FeaturedImage = fileName;
                    }
                    catch { }
                }

                news.UpdatedDate = DateTime.Now;
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(new[] { "Tin tức", "Sự kiện", "Chia sẻ" });
            return View(news);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
