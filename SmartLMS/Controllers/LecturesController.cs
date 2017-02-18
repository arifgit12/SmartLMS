using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartLMS.Models;
using Microsoft.AspNet.Identity;
using System.IO;
using SmartLMS.Infrastructure.FileHelpers;

namespace SmartLMS.Controllers
{
    [Authorize(Roles = "Lecturer")]
    public class LecturesController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public LecturesController(ApplicationUserManager userManager) : base(userManager)
        {

        }

        // GET: Lectures
        public async Task<ActionResult> Index()
        {
            ApplicationUser user = GetUser();
            var lectures = db.Lectures.Where( l => l.User.Id == user.Id ).Include(l => l.Course);
            return View(await lectures.ToListAsync());
        }

        // GET: Lectures/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecture lecture = await db.Lectures.FindAsync(id);
            if (lecture == null)
            {
                return HttpNotFound();
            }
            string getuser = User.Identity.GetUserId();
            lecture.User = db.Users.Where(u => u.Id == getuser).Single();
            db.Lectures.Add(lecture);

            string coursename = db.Courses.Where(c => c.CourseId == lecture.CourseId).Single().CourseName;
            string uploadpath = Path.Combine(Server.MapPath("~/Content/Uploads/Lecturers/"), User.Identity.GetUserName(), coursename, lecture.LectureName + ".mp4");
            ViewData["lecture"] = uploadpath;
            return View(lecture);
        }

        // GET: Lectures/Create
        public ActionResult Create()
        {
            string getuser = User.Identity.GetUserId();

            ViewBag.CourseId = new SelectList(db.Courses.Where(c => c.User.Id == getuser).ToList(), "CourseId", "CourseName");
            //ViewBag.ApplicationUserID = new SelectList(getuser, "Id", "Email");
            return View();
        }

        // POST: Lectures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Lecture lecture, HttpPostedFileBase upload)
        {
            string getuser = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                lecture.User = db.Users.Where(u => u.Id == getuser).Single();

                if (upload.ContentLength > 0)
                {
                    string coursename = db.Courses.Where(c => c.CourseId == lecture.CourseId).Single().CourseName;
                    string uploadedFileName = FileUtils.UploadFile(upload, User.Identity.GetUserName(), coursename);

                    lecture.FileName = uploadedFileName;

                    db.Lectures.Add(lecture);
                    await db.SaveChangesAsync();
                    //string uploadDirectoryPath = Path.Combine(Server.MapPath("~/Content/Uploads/Lecturers/"), User.Identity.GetUserName(), coursename);
                    //if (!Directory.Exists(uploadDirectoryPath))
                    //{
                    //    Directory.CreateDirectory(uploadDirectoryPath);
                    //}
                    //string uploadpath = Path.Combine(Server.MapPath("~/Content/Uploads/Lecturers/"), User.Identity.GetUserName(), coursename, lecture.LectureName + ".mp4");
                    //upload.SaveAs(uploadpath);
                }

                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", lecture.CourseId);
            //ViewBag.ApplicationUserID = new SelectList(db.ApplicationUsers, "Id", "Email", lecture.ApplicationUserID);
            return View(lecture);
        }

        // GET: Lectures/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecture lecture = await db.Lectures.FindAsync(id);
            if (lecture == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", lecture.CourseId);
            //ViewBag.ApplicationUserID = new SelectList(db.ApplicationUsers, "Id", "Email", lecture.ApplicationUserID);
            return View(lecture);
        }

        // POST: Lectures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Lecture lecture, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload.ContentLength > 0)
                {
                    string coursename = db.Courses.Where(c => c.CourseId == lecture.CourseId).Single().CourseName;
                    //string uploadpath = Path.Combine(Server.MapPath("~/Content/Uploads/Lecturers/"), User.Identity.GetUserName(), coursename, lecture.LectureName + ".mp4");
                    //upload.SaveAs(uploadpath);
                    string uploadedFileName = FileUtils.UploadFile(upload, User.Identity.GetUserName(), coursename);

                    lecture.FileName = uploadedFileName;
                }

                db.Entry(lecture).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", lecture.CourseId);
            return View(lecture);
        }

        // GET: Lectures/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecture lecture = await db.Lectures.FindAsync(id);
            if (lecture == null)
            {
                return HttpNotFound();
            }
            return View(lecture);
        }

        // POST: Lectures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Lecture lecture = await db.Lectures.FindAsync(id);
            db.Lectures.Remove(lecture);
            string coursename = db.Courses.Where(c => c.CourseId == lecture.CourseId).Single().CourseName;
            string uploadpath = Path.Combine(Server.MapPath("~/Content/Uploads/Lecturers/"), User.Identity.GetUserName(), coursename, lecture.LectureName + ".mp4");
            System.IO.File.Delete(uploadpath);

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
