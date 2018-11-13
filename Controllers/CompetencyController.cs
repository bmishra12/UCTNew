using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCT.Models;
using System.Data.SqlClient;

namespace UCT.Controllers
{
    public class CompetencyController : Controller
    {
        private UCTContext db = new UCTContext();

        //
        // GET: /Competency/

        public ActionResult Index()
        {
            return View(db.Competencies.ToList());
        }

        //
        // GET: /Competency/Details/5

        public ActionResult Details(int id = 0)
        {
            Competency competency = db.Competencies.Find(id);
            if (competency == null)
            {
                return HttpNotFound();
            }
            return View(competency);
        }

        //
        // GET: /Competency/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Competency/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Competency competency)
        {
            if (ModelState.IsValid)
            {
                try
                { db.Competencies.Add(competency);
                db.SaveChanges();

                return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", String.Format("Unable to create competency, a competency with same name may already exist", e.InnerException));
                }
                
               
            }

            return View(competency);
        }

        //
        // GET: /Competency/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Competency competency = db.Competencies.Find(id);
            if (competency == null)
            {
                return HttpNotFound();
            }
            return View(competency);
        }

        //
        // POST: /Competency/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Competency competency)
        {
            if (ModelState.IsValid)
            {
                db.Entry(competency).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(competency);
        }

        //
        // GET: /Competency/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Competency competency = db.Competencies.Find(id);
            if (competency == null)
            {
                return HttpNotFound();
            }
            return View(competency);
        }

        //
        // POST: /Competency/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Competency competency = db.Competencies.Find(id);
            db.Competencies.Remove(competency);
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