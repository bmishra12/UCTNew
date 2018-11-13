using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UCT.Models;
using WebMatrix.WebData;
using UCT.Filters;

namespace UCT.Controllers
{
    public class ReportController : Controller
    {
        private UCTContext db = new UCTContext();

        //
        // GET: /Report/

        public ActionResult Index()
        {
            return View(db.Programs.ToList());
        }

        //
        // GET: /Report/Details/5

        public ActionResult Details(int id = 0)
        {
            Program program = db.Programs.Find(id);
            if (program == null)
            {
                return HttpNotFound();
            }
            return View(program);
        }

        //
        // GET: /Report/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Report/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Program program)
        {
            if (ModelState.IsValid)
            {
                db.Programs.Add(program);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(program);
        }

        //
        // GET: /Report/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Program program = db.Programs.Find(id);
            if (program == null)
            {
                return HttpNotFound();
            }
            return View(program);
        }

        //
        // POST: /Report/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Program program)
        {
            if (ModelState.IsValid)
            {
                db.Entry(program).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(program);
        }

        //
        // GET: /Report/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Program program = db.Programs.Find(id);
            if (program == null)
            {
                return HttpNotFound();
            }
            return View(program);
        }
        //
        // GET: /Program/Report/5
        //[Authorize(Roles = "SuperUser")]
        public ActionResult Report(int id = 0)
        {

            string[] r = Roles.GetAllRoles();
            User.IsInRole("ProgramUser");
            Roles.RoleExists("ProgramAdmin");
            //how to add role to a user..
            //// if (!Roles.GetRolesForUser("sallen").Contains("Admin"))
            ////////{
            ////////    Roles.AddUsersToRoles(new[] { "sallen" }, new[] { "admin" });
            ////////}

            int year = 2003;
            DataTable dt = GetCompetenciesByProgramSPCall(id, year);


            return View(dt);
        }

        //
        //
        // POST: /Report/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Program program = db.Programs.Find(id);
            db.Programs.Remove(program);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        private DataTable GetCompetenciesByProgramSPCall(int id, int year)
        {
            DataTable dt = new DataTable();
            using (db)
            {
                db.Database.Connection.Open();
                DbCommand cmd = db.Database.Connection.CreateCommand();
                cmd.CommandText = "[dbo].[GetCompetenciesByProgram]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("ProgramID", id));
                cmd.Parameters.Add(new SqlParameter("year", year));

                using (var reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);

                }


                return dt;
            }
        }
    }
}