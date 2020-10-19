using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Training_FPT0.Models;

namespace Training_FPT0.Controllers
{
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }
        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        // GET: Categories/Create
        [HttpGet]
        [Authorize(Roles ="TrainingStaff")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Create");
            }
            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Categories/Edit/5
        [HttpGet]
        [Authorize(Roles = "TrainingStaff")]

        public ActionResult Edit(int id)
        {
            var categoryInDb = db.Categories.SingleOrDefault(p => p.Id == id);
            if (categoryInDb == null)
            {
                return HttpNotFound();
            }
            return View(categoryInDb);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [Authorize(Roles = "TrainingStaff")]

        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            var categoryInDb = db.Categories.SingleOrDefault(p => p.Id == category.Id);
            if (categoryInDb == null)
            {
                return HttpNotFound();
            }

            categoryInDb.Name = category.Name;
            categoryInDb.Description = category.Description;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Categories/Delete/5
        [HttpGet]
        [Authorize(Roles = "TrainingStaff")]

        public ActionResult Delete(int id)
        {
            var categoryInDb = db.Categories.SingleOrDefault(p => p.Id == id);
            if (categoryInDb == null)
            {
                return HttpNotFound();
            }
            db.Categories.Remove(categoryInDb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}