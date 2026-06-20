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
    public class FamilyMembersController : Controller
    {
        private FamilyTreeContext db = new FamilyTreeContext();

        // GET: FamilyMembers
        public ActionResult Index(string searchName = "", string searchGender = "", string searchYear = "")
        {
            var members = db.FamilyMembers.AsQueryable();

            // Tìm kiếm theo tên
            if (!string.IsNullOrEmpty(searchName))
            {
                members = members.Where(m => m.FullName.Contains(searchName));
            }

            // Lọc theo giới tính
            if (!string.IsNullOrEmpty(searchGender))
            {
                members = members.Where(m => m.Gender == searchGender);
            }

            // Lọc theo năm sinh
            if (!string.IsNullOrEmpty(searchYear))
            {
                int year = int.Parse(searchYear);
                members = members.Where(m => m.DateOfBirth.Year == year);
            }

            ViewBag.SearchName = searchName;
            ViewBag.SearchGender = searchGender;
            ViewBag.SearchYear = searchYear;

            return View(members.OrderByDescending(m => m.CreatedDate).ToList());
        }

        // GET: FamilyMembers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            FamilyMember member = db.FamilyMembers
                .Include(m => m.Father)
                .Include(m => m.Mother)
                .Include(m => m.Spouse)
                .Include(m => m.Children)
                .FirstOrDefault(m => m.Id == id);

            if (member == null)
                return HttpNotFound();

            return View(member);
        }

        // GET: FamilyMembers/Create
        public ActionResult Create()
        {
            ViewBag.FatherId = new SelectList(db.FamilyMembers.Where(m => m.Gender == "Nam"), "Id", "FullName");
            ViewBag.MotherId = new SelectList(db.FamilyMembers.Where(m => m.Gender == "Nữ"), "Id", "FullName");
            ViewBag.SpouseId = new SelectList(db.FamilyMembers, "Id", "FullName");
            return View();
        }

        // POST: FamilyMembers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FullName,DateOfBirth,Gender,PhoneNumber,Email,Address,Occupation,FatherId,MotherId,SpouseId")] FamilyMember member, HttpPostedFileBase profileImage)
        {
            if (ModelState.IsValid)
            {
                // Xử lý upload ảnh
                if (profileImage != null && profileImage.ContentLength > 0)
                {
                    try
                    {
                        string fileName = Path.GetFileNameWithoutExtension(profileImage.FileName);
                        string extension = Path.GetExtension(profileImage.FileName);
                        fileName = fileName + "_" + DateTime.Now.Ticks + extension;
                        string path = Path.Combine(Server.MapPath("~/Uploads/Members"), fileName);

                        if (!Directory.Exists(Server.MapPath("~/Uploads/Members")))
                            Directory.CreateDirectory(Server.MapPath("~/Uploads/Members"));

                        profileImage.SaveAs(path);
                        member.ProfileImage = fileName;
                    }
                    catch { }
                }

                member.CreatedDate = DateTime.Now;
                db.FamilyMembers.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FatherId = new SelectList(db.FamilyMembers.Where(m => m.Gender == "Nam"), "Id", "FullName", member.FatherId);
            ViewBag.MotherId = new SelectList(db.FamilyMembers.Where(m => m.Gender == "Nữ"), "Id", "FullName", member.MotherId);
            ViewBag.SpouseId = new SelectList(db.FamilyMembers, "Id", "FullName", member.SpouseId);
            return View(member);
        }

        // GET: FamilyMembers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            FamilyMember member = db.FamilyMembers.Find(id);
            if (member == null)
                return HttpNotFound();

            ViewBag.FatherId = new SelectList(db.FamilyMembers.Where(m => m.Gender == "Nam" && m.Id != id), "Id", "FullName", member.FatherId);
            ViewBag.MotherId = new SelectList(db.FamilyMembers.Where(m => m.Gender == "Nữ" && m.Id != id), "Id", "FullName", member.MotherId);
            ViewBag.SpouseId = new SelectList(db.FamilyMembers.Where(m => m.Id != id), "Id", "FullName", member.SpouseId);
            return View(member);
        }

        // POST: FamilyMembers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,DateOfBirth,Gender,PhoneNumber,Email,Address,Occupation,ProfileImage,FatherId,MotherId,SpouseId,CreatedDate")] FamilyMember member, HttpPostedFileBase profileImage)
        {
            if (ModelState.IsValid)
            {
                // Xử lý upload ảnh
                if (profileImage != null && profileImage.ContentLength > 0)
                {
                    try
                    {
                        string oldPath = Path.Combine(Server.MapPath("~/Uploads/Members"), member.ProfileImage);
                        if (System.IO.File.Exists(oldPath))
                            System.IO.File.Delete(oldPath);

                        string fileName = Path.GetFileNameWithoutExtension(profileImage.FileName);
                        string extension = Path.GetExtension(profileImage.FileName);
                        fileName = fileName + "_" + DateTime.Now.Ticks + extension;
                        string path = Path.Combine(Server.MapPath("~/Uploads/Members"), fileName);

                        profileImage.SaveAs(path);
                        member.ProfileImage = fileName;
                    }
                    catch { }
                }

                member.UpdatedDate = DateTime.Now;
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FatherId = new SelectList(db.FamilyMembers.Where(m => m.Gender == "Nam" && m.Id != member.Id), "Id", "FullName", member.FatherId);
            ViewBag.MotherId = new SelectList(db.FamilyMembers.Where(m => m.Gender == "Nữ" && m.Id != member.Id), "Id", "FullName", member.MotherId);
            ViewBag.SpouseId = new SelectList(db.FamilyMembers.Where(m => m.Id != member.Id), "Id", "FullName", member.SpouseId);
            return View(member);
        }

        // GET: FamilyMembers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            FamilyMember member = db.FamilyMembers.Find(id);
            if (member == null)
                return HttpNotFound();

            return View(member);
        }

        // POST: FamilyMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FamilyMember member = db.FamilyMembers.Find(id);
            db.FamilyMembers.Remove(member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
