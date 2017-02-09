using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartLMS.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using SmartLMS.Infrastructure.Video;

namespace SmartLMS.Controllers
{
    [Authorize(Roles = "User")]
    public class StudentDashboardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentDashboard
        public ActionResult Index()
        {
            ViewData["user"] = db.Users.ToList().Count();
            ViewData["course"] = db.Courses.ToList().Count();
            ViewData["Lecture"] = db.Lectures.ToList().Count();
            return View();
        }

        // GET: StudentDashboard/Dashboard
        public ActionResult Dashboard()
        {
            ViewData["user"] = db.Users.ToList().Count();
            ViewData["course"] = db.Courses.ToList().Count();
            ViewData["Lecture"] = db.Lectures.ToList().Count();
            return View();
        }

        public ActionResult Courses()
        {
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ViewData["Student"] = userManager.FindByNameAsync(User.Identity.Name).Result.Email;
            var crs = db.Courses.Include(c => c.Category).Include(c => c.User);
            ViewData["StdCrs"] = userManager.FindByNameAsync(User.Identity.Name).Result.Id;
            return View(crs);
        }

        public ActionResult Enroll(String Student, int Course)
        {
            var store = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(store);

            ApplicationUser student = userManager.FindByEmailAsync(Student).Result;
            Course course = db.Courses.Where(x => x.CourseId == Course).Single();
            var StudentCourse = new StudentCourse();
            StudentCourse.Course = course;
            StudentCourse.Student = student;
            StudentCourse.CourseId = course.CourseId;
            StudentCourse.StudentId = student.Id;
            db.StudentCourses.Add(StudentCourse);
            db.SaveChanges();
            return RedirectToAction("Courses");
        }

        public ActionResult Lectures()
        {
            var store = User.Identity.GetUserId();
            var getCourse = db.StudentCourses.Where(x => x.StudentId == store).ToList();
            List<Course> List = new List<Course>();
            foreach (var crs in getCourse)
            {
                var course = db.Courses.Include(c => c.Category).Include(c => c.User).Where(y => y.CourseId == crs.CourseId).Single();
                List.Add(course);
            }

            return View(List);
        }

        public ActionResult SeeLectures(string course, string email)
        {

            Dictionary<string, string> files = new Dictionary<string, string>();
            string[] file = Directory.GetFiles(Server.MapPath("~/Content/Uploads/Lecturers/" + email + "/" + course), "*.*", SearchOption.AllDirectories);


            foreach (string i in file)
            {
                files.Add(i, Path.GetFileName(i));

            }
            ViewData["filename"] = files;
            return View();

        }


        public ActionResult WatchLecture(string path)
        {
            ViewData["path"] = path;
            return View();
        }

        public ActionResult DownloadLecture(string path)
        {
            return new VideoResult(path);
        }

        public ActionResult Quiz()
        {
            var store = User.Identity.GetUserId();
            var getCourse = db.StudentCourses.Where(x => x.StudentId == store).ToList();
            List<Course> List = new List<Course>();
            foreach (var crs in getCourse)
            {
                var course = db.Courses.Include(c => c.Category).Include(c => c.User).Where(y => y.CourseId == crs.CourseId).Single();
                List.Add(course);
            }

            return View(List);
        }

        public ActionResult SeeQuiz(int course)
        {
            //var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            //var userManager = new UserManager<ApplicationUser>(store);
            //ViewData["Student"] = userManager.FindByNameAsync(User.Identity.Name).Result;
            ApplicationDbContext db = new ApplicationDbContext();

            ViewData["std"] = User.Identity.GetUserId();


            var Quizez = db.Quiz.Include(s => s.Students).Where(x => x.CourseId == course).ToList();
            return View(Quizez);
        }

        public ActionResult TakeQuiz(int quiz)
        {
            var quest = db.Questions.Include(x => x.AnswerChoices).Where(m => m.QuizId == quiz).ToList();
            ViewData["quizId"] = quiz;
            return View(quest);
        }

        [HttpPost]
        public ActionResult CalculateMarks(int[] Choices, List<Question> question, int quizId)
        {
            var quiz = new StudentQuiz();
            var store = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(store);
            quiz.Student = userManager.FindByNameAsync(User.Identity.Name).Result;
            quiz.StudentId = User.Identity.GetUserId();

            int i = 0;

            int totalMarks = db.Quiz.Where(x => x.QuizId == quizId).Single().Score;
            double marksPerQuestion = totalMarks / question.Count();
            double obtainedMarks = 0;

            foreach (var item in question)
            {
                var q = db.Questions.Include(y => y.AnswerChoices).Where(x => x.QuestionId == item.QuestionId).Single();
                foreach (var choice in q.AnswerChoices)
                {
                    if (choice.IsCorrect == true)
                    {
                        if (Choices[i] == choice.AnswerChoiceId)
                        {
                            obtainedMarks += marksPerQuestion;

                        }

                    }

                }
                i++;
            }
            quiz.QuizId = quizId;
            quiz.Quiz = db.Quiz.Where(x => x.QuizId == quizId).Single();
            quiz.Marks = obtainedMarks;
            db.StudentQuiz.Add(quiz);
            db.SaveChanges();
            ViewData["marks"] = obtainedMarks;
            return View();
        }

        public ActionResult Assignments()
        {
            var store = User.Identity.GetUserId();
            var getCourse = db.StudentCourses.Where(x => x.StudentId == store).ToList();
            List<Course> List = new List<Course>();
            foreach (var crs in getCourse)
            {
                var course = db.Courses.Include(c => c.Category).Include(c => c.User).Where(y => y.CourseId == crs.CourseId).Single();
                List.Add(course);
            }

            return View(List);

        }

        public ActionResult SeeAssignment(int course)
        {
            //var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            //var userManager = new UserManager<ApplicationUser>(store);
            //ViewData["Student"] = userManager.FindByNameAsync(User.Identity.Name).Result;
            ApplicationDbContext db = new ApplicationDbContext();

            ViewData["std"] = User.Identity.GetUserId();


            var Assignment = db.Assignment.Include(s => s.Students).Where(x => x.CourseId == course).ToList();
            return View(Assignment);
        }



        public ActionResult PracticeCode()
        {
            return View();
        }


        public ActionResult SendRating(string r, string id, string url)
        {
            try
            {
                //var context = new ApplicationDbContext();

                string currentUserId = User.Identity.GetUserId();

                int autoid = 0;
                Int16 thisVote = 0;
                Int16.TryParse(r, out thisVote);
                int.TryParse(id, out autoid);

                if (!User.Identity.IsAuthenticated)
                {
                    return Json("Not authenticated!");
                }

                switch (r)
                {

                    case "5":
                    case "4":
                    case "3":
                    case "2":
                    case "1":

                        var userid = User.Identity.GetUserId();
                        var rating = db.Ratings.Where(o => o.CourseId == autoid && o.UserId == userid).SingleOrDefault();

                        if (rating != null)
                        {
                            HttpCookie Cookie = new HttpCookie(url, "true");
                            Response.Cookies.Add(Cookie);
                            return Json("<br/> You havr already rate ,Thanks!...", JsonRequestBehavior.AllowGet);

                        }
                        var rating2 = new Rating();
                        var store = new UserStore<ApplicationUser>(db);
                        var userManager = new UserManager<ApplicationUser>(store);
                        var user = userManager.FindByName(User.Identity.Name);


                        rating2.User = user;
                        rating2.UserId = userid;
                        rating2.CourseId = autoid;
                        rating2.Course = db.Courses.Where(x => x.CourseId == autoid).Single();

                        rating2.stars = thisVote;
                        db.Ratings.Add(rating2);
                        if (db.SaveChanges() > 0)
                        {
                            HttpCookie cookie = new HttpCookie(url, "true");
                            Response.Cookies.Add(cookie);

                            return Json("<br />You rated " + r + " star(s), thanks !", JsonRequestBehavior.AllowGet);

                        }


                        break;
                    default:
                        break;
                }
                return Json("Rating Failed!", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                HttpCookie cookie = new HttpCookie(url, "true");
                Response.Cookies.Add(cookie);

                return Json(ex.Message, JsonRequestBehavior.AllowGet);

                throw;
            }
        }
        
        public ActionResult VoteNow(int id)
        {
            ViewData["id"] = id;
            return View();
        }
    }
}