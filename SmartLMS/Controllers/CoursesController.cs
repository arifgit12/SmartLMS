using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SmartLMS.Models;
using Microsoft.AspNet.Identity;
using SmartLMS.Data.Repository;
using SmartLMS.ViewModels;
using System.Text.RegularExpressions;

namespace SmartLMS.Controllers
{
    [Authorize(Roles = "Lecturer")]
    public class CoursesController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        protected ISmartLMSData Data { get; private set; }
        public CoursesController(ISmartLMSData data)
        {
            this.Data = data;
        }

        // GET: Courses
        public ActionResult Index()
        {
            string getuser = User.Identity.GetUserId();
            var courses = this.Data.Courses.SearchFor(c => c.User.Id == getuser).ToList();
            var model = AutoMapper.Mapper.Map<IEnumerable<CourseViewModel>>(courses);
            return View(model);
        }

        // GET: Courses/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = this.Data.Courses.SearchFor(d => d.CourseId == id.Value).FirstOrDefault();

            if (course == null)
            {
                return HttpNotFound();
            }
            var model = AutoMapper.Mapper.Map<CourseViewModel>(course);
            return View(model);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(this.Data.Categories.All(), "CategoryId", "CategoryName");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel model)
        {
            string getuser = User.Identity.GetUserId();
            
            if (ModelState.IsValid)
            {
                Course course = AutoMapper.Mapper.Map<Course>(model);
                course.CourseCode = Regex.Replace(model.CourseName, @"\s", "");
                course.User = this.Data.Users.SearchFor(u => u.Id == getuser).SingleOrDefault();
                this.Data.Courses.Add(course);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(this.Data.Categories.All(), "CategoryId", "CategoryName", model.CategoryId);
            ModelState.AddModelError(String.Empty, "Course already Created. Choose different Name");

            return View(model);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = this.Data.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(this.Data.Categories.All(), "CategoryId", "CategoryName", course.CategoryId);
            var model = AutoMapper.Mapper.Map<CourseViewModel>(course);

            return View(model);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CourseViewModel model)
        {
            string getuser = User.Identity.GetUserId();
            model.Category = this.Data.Categories.Find(model.CategoryId);
            if (ModelState.IsValid)
            {
                Course course = AutoMapper.Mapper.Map<Course>(model);
                course.CourseCode = Regex.Replace(model.CourseName, @"\s", "");
                // Rename the file uploads
                course.User = this.Data.Users.SearchFor(u => u.Id == getuser).SingleOrDefault();
                this.Data.Courses.Update(course);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Unable to edit");
            ModelState.AddModelError(String.Empty, "Course already Created. Choose different Name");
            ViewBag.CategoryId = new SelectList(this.Data.Categories.All(), "CategoryId", "CategoryName", model.CategoryId);
            return View(model);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = this.Data.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }

            var model = AutoMapper.Mapper.Map<CourseViewModel>(course);
            return View(model);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = this.Data.Courses.Find(id);
            this.Data.Courses.Delete(course);
            this.Data.SaveChanges();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
