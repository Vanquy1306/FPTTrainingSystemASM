using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Training_FPT0.Models;

namespace Training_FPT0.Controllers
{
    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Courses
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.Category)
                .Include(c => c.Topic);
            return View(courses.ToList());
        }
        [HttpGet]
        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }
        [HttpGet]
        [Authorize(Roles = "TrainingStaff")]
        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Name");
            return View();
        }

        // POST: Courses/Create
        [HttpPost]
        [Authorize(Roles = "TrainingStaff")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", course.CategoryId);
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Name", course.TopicId);
            return View(course);
        }
        [HttpGet]
        [Authorize(Roles = "TrainingStaff")]
        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", course.CategoryId);
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Name", course.TopicId);
            return View(course);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [Authorize(Roles = "TrainingStaff")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", course.CategoryId);
            ViewBag.TopicId = new SelectList(db.Topics, "Id", "Name", course.TopicId);
            return View(course);
        }
        [HttpGet]
        [Authorize(Roles = "TrainingStaff")]
        // GET: Courses/Delete/5
        public ActionResult Delete(int id)
        {
            var courseInDb = db.Courses.SingleOrDefault(p => p.Id == id);
            if (courseInDb == null)
            {
                return HttpNotFound();
            }
            db.Courses.Remove(courseInDb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}