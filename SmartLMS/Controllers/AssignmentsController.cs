using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartLMS.Models;
using System.Net;
using Microsoft.AspNet.Identity;
using SmartLMS.Infrastructure.FileHelpers;
using System.IO;

namespace SmartLMS.Controllers
{
    [Authorize(Roles = "Lecturer")]
    public class AssignmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Assignments
        [AllowAnonymous]
        public ActionResult Index()
        {
            var assignment = db.Assignment.Include(a => a.Course);
            return View(assignment.ToList());
        }

        // GET: Assignments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignment.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            string coursename = db.Courses.Where(c => c.CourseId == assignment.CourseId).Single().CourseName;
            ViewBag.FilePath = Server.MapPath(FileUtils.UPLOAD_PATH + User.Identity.GetUserName() + "/" + coursename + "/" + assignment.FileName);

            return View(assignment);
        }

        // GET: Assignments/Create
        public ActionResult Create()
        {
            string userid = User.Identity.GetUserId();

            ViewBag.CourseId = new SelectList(db.Courses.Where(x => x.User.Id == userid), "CourseId", "CourseName");
            return View();
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Assignment assignment, HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                var course = db.Courses.Find(assignment.CourseId);

                if(course.Lectures.Count() > 0)
                {
                    string coursename = db.Courses.Where(c => c.CourseId == assignment.CourseId).Single().CourseName;
                    //string ext = Path.GetExtension(upload.FileName);
                    //string uploadpath = Path.Combine(Server.MapPath("~/Content/Uploads/Lecturers/"), User.Identity.GetUserName(), coursename, assignment.AssignmentName + ext);
                    //upload.SaveAs(uploadpath);
                    string uploadedFileName = FileUtils.UploadFile(upload, User.Identity.GetUserName(), coursename);

                    assignment.FileName = uploadedFileName;

                    db.Assignment.Add(assignment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            TempData["Error"] = "Unable to Add Assignments";
            string userid = User.Identity.GetUserId();
            ViewBag.CourseId = new SelectList(db.Courses.Where(x => x.User.Id == userid), "CourseId", "CourseName");
            return View(assignment);
        }

        // GET: Assignments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignment.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            string userid = User.Identity.GetUserId();
            string coursename = db.Courses.Where(c => c.CourseId == assignment.CourseId).Single().CourseName;

            ViewBag.CourseId = new SelectList(db.Courses.Where(x => x.User.Id == userid), "CourseId", "CourseName");
            return View(assignment);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Assignment assignment, HttpPostedFileBase upload)
        {
            string coursename = db.Courses.Where(c => c.CourseId == assignment.CourseId).Single().CourseName;

            if (ModelState.IsValid)
            {
                if ( upload != null && upload.ContentLength > 0)
                {
                    string uploadedFileName = FileUtils.UploadFile(upload, User.Identity.GetUserName(), coursename);

                    assignment.FileName = uploadedFileName;
                }
                db.Entry(assignment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", assignment.CourseId);
            return View(assignment);
        }

        // GET: Assignments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignment.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assignment assignment = db.Assignment.Find(id);
            db.Assignment.Remove(assignment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public FileResult Download(string FileName)
        {
            string contentType = string.Empty;

            if (FileName.Contains(".pdf"))
            {
                contentType = "application/pdf";
            }

            else if (FileName.Contains(".docx"))
            {
                contentType = "application/docx";
            }

            return File(FileName,contentType);
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