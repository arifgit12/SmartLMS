using Microsoft.AspNet.Identity;
using SmartLMS.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SmartLMS.Controllers
{
    [Authorize(Roles = "Lecturer")]
    public class QuizsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Quizs
        public ActionResult Index()
        {
            string getuser = User.Identity.GetUserId();
            var quiz = db.Quiz.Include(q => q.Course).Where(c => c.Course.User.Id == getuser);
            return View(quiz.ToList());
        }

        // GET: Quizs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quiz.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        // GET: Quizs/Create
        public ActionResult Create()
        {
            string getuser = User.Identity.GetUserId();
            ViewBag.CourseId = new SelectList(db.Courses.Where(c => c.User.Id == getuser).ToList(), "CourseId", "CourseName");
            return View();
        }

        // POST: Quizs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                db.Quiz.Add(quiz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            string getuser = User.Identity.GetUserId();
            ViewBag.CourseId = new SelectList(db.Courses.Where(c => c.User.Id == getuser).ToList(), "CourseId", "CourseName");
            return View(quiz);
        }

        // GET: Quizs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quiz.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            string getuser = User.Identity.GetUserId();
            ViewBag.CourseId = new SelectList(db.Courses.Where(c => c.User.Id == getuser).ToList(), "CourseId", "CourseName", quiz.CourseId);
            return View(quiz);
        }

        // POST: Quizs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quiz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            string getuser = User.Identity.GetUserId();
            ViewBag.CourseId = new SelectList(db.Courses.Where(c => c.User.Id == getuser).ToList(), "CourseId", "CourseName", quiz.CourseId);
            return View(quiz);
        }

        // GET: Quizs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quiz.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        // POST: Quizs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quiz quiz = db.Quiz.Find(id);
            db.Quiz.Remove(quiz);
            db.SaveChanges();
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