using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SmartLMS.Models;
using SmartLMS.Data.Repository;
using SmartLMS.ViewModels;

namespace SmartLMS.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CategoriesController : Controller
    {
        protected ISmartLMSData Data { get; private set; }

        public CategoriesController(ISmartLMSData data)
        {
            this.Data = data;
        }

        // GET: Categories
        [AllowAnonymous]
        public ActionResult Index()
        {
            var model = AutoMapper.Mapper.Map<IEnumerable<CategoryViewModel>>(this.Data.Categories.All());
            return View(model);
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = this.Data.Categories.Find(id);
            
            if (category == null)
            {
                return HttpNotFound();
            }

            var model = AutoMapper.Mapper.Map<CategoryViewModel>(category);
            return View(model);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            ViewData["Categories"] = new SelectList(this.Data.Categories.All().ToList(), "CategoryId", "CategoryName", "");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = AutoMapper.Mapper.Map<Category>(model);
                this.Data.Categories.Add(category);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = this.Data.Categories.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            var model = AutoMapper.Mapper.Map<CategoryViewModel>(category);
            return View(model);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = AutoMapper.Mapper.Map<Category>(model);
                this.Data.Categories.Update(category);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = this.Data.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            var model = AutoMapper.Mapper.Map<CategoryViewModel>(category);
            return View(model);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = this.Data.Categories.Find(id);
            this.Data.Categories.Delete(category);
            this.Data.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
