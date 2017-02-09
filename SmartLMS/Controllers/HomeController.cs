using SmartLMS.Models;
using System.Data;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace SmartLMS.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            if (User.Identity.IsAuthenticated)
            {
                //if (User.IsInRole("Student"))
                //{
                //    return RedirectToAction("Dashboard", "StudentDashboard");
                //}

                //else
                //{
                //    return RedirectToAction("Dashboard", "LecturerDashboard");
                //}

            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<ActionResult> Course()
        {
            var courses = db.Courses.Include(c => c.Category);
            return View(await courses.ToListAsync());
        }
    }
}
