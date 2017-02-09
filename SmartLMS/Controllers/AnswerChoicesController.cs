using SmartLMS.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Net;

namespace SmartLMS.Controllers
{
    [Authorize(Roles = "Lecturer")]
    public class AnswerChoicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AnswerChoices
        public ActionResult Index()
        {
            var answerchoices = db.Answerchoices.Include(a => a.Question).Include(a => a.Question.Quiz).Where(x => x.Question.Quiz.Course.User.UserName == User.Identity.Name).ToList();
            return View(answerchoices.ToList());
        }

        // GET: AnswerChoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnswerChoice answerChoice = db.Answerchoices.Find(id);
            if (answerChoice == null)
            {
                return HttpNotFound();
            }
            return View(answerChoice);
        }

        // GET: AnswerChoices/Create
        public ActionResult Create()
        {
            ViewBag.QuestionId = new SelectList(from s in db.Questions.Include(m => m.Quiz).ToList()
                                        select new
                                        {
                                            QuestionId = s.QuestionId,
                                            QuestionText = s.QuestionText + "-" + s.Quiz.QuizName
                                        }, "QuestionId", "QuestionText");
            return View();
        }

        // POST: AnswerChoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AnswerChoiceId,Choices,IsCorrect,QuestionId")] AnswerChoice answerChoice)
        {
            if (ModelState.IsValid)
            {
                db.Answerchoices.Add(answerChoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionId = new SelectList(from s in db.Questions.Include(m => m.Quiz).ToList()
                                        select new
                                        {
                                            QuestionId = s.QuestionId,
                                            QuestionText = s.QuestionText + "-" + s.Quiz.QuizName
                                        }, "QuestionId", "QuestionText");
            return View(answerChoice);
        }

        // GET: AnswerChoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnswerChoice answerChoice = db.Answerchoices.Find(id);
            if (answerChoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionId = new SelectList(from s in db.Questions.Include(m => m.Quiz).ToList()
                                        select new
                                        {
                                            QuestionId = s.QuestionId,
                                            QuestionText = s.QuestionText + "-" + s.Quiz.QuizName
                                        }, "QuestionId", "QuestionText");
            return View(answerChoice);
        }

        // POST: AnswerChoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AnswerChoiceId,Choices,IsCorrect,QuestionId")] AnswerChoice answerChoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(answerChoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionId = new SelectList(from s in db.Questions.Include(m => m.Quiz).ToList()
                                        select new
                                        {
                                            QuestionId = s.QuestionId,
                                            QuestionText = s.QuestionText + "-" + s.Quiz.QuizName
                                        }, "QuestionId", "QuestionText");
            return View(answerChoice);
        }

        // GET: AnswerChoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnswerChoice answerChoice = db.Answerchoices.Find(id);
            if (answerChoice == null)
            {
                return HttpNotFound();
            }
            return View(answerChoice);
        }

        // POST: AnswerChoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AnswerChoice answerChoice = db.Answerchoices.Find(id);
            db.Answerchoices.Remove(answerChoice);
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