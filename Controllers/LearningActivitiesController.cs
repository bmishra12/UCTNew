using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCT.Models;

namespace UCT.Controllers
{
    public class LearningActivitiesController : Controller
    {
       
        private UCTContext db = new UCTContext();

        //
        // GET: /LearningActivities/

        public ActionResult Index()
        {
            return View(db.LearningActivities.ToList());
        }

        //
        // GET: /LearningActivities/Details/5

        public ActionResult Details(int id = 0)
        {
            LearningActivity learningactivity = db.LearningActivities.Find(id);
            if (learningactivity == null)
            {
                return HttpNotFound();
            }
            return View(learningactivity);
        }

        //
        // GET: /LearningActivities/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /LearningActivities/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LearningActivity learningactivity)




        {
            if (ModelState.IsValid)


                try
                {
                    db.LearningActivities.Add(learningactivity);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", String.Format("Unable to create learning activity, a learning activity with same name may already exist", e.InnerException));
                }

        

            return View(learningactivity);
        }

        //
        // GET: /LearningActivities/Edit/5

        public ActionResult Edit(int id = 0)
        {
            LearningActivity learningactivity = db.LearningActivities.Find(id);
            if (learningactivity == null)
            {
                return HttpNotFound();
            }
            return View(learningactivity);
        }

        //
        // POST: /LearningActivities/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LearningActivity learningactivity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(learningactivity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(learningactivity);
        }

        //
        // GET: /LearningActivities/Delete/5

        public ActionResult Delete(int id = 0)
        {
            LearningActivity learningactivity = db.LearningActivities.Find(id);
            if (learningactivity == null)
            {
                return HttpNotFound();
            }
            return View(learningactivity);
        }

        //
        // POST: /LearningActivities/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LearningActivity learningactivity = db.LearningActivities.Find(id);
            db.LearningActivities.Remove(learningactivity);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}