using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCT.Models;
using UCT.ViewModels;

namespace UCT.Controllers
{
    public class ProgramCompetenciesController : Controller
    {
        private UCTContext context = new UCTContext();


        //
        // GET: /ProgramLearningActivities/

        public ActionResult Index()
        {
            var programlearningactivities = context.ProgramLearningActivities.Include(p => p.LearningActivity).Include(p => p.Program);
            return View(programlearningactivities.ToList());
        }


        //
        // GET: /ProgramCompetecies/Details/5

        public ActionResult Details(int id = 0)
        {
            ProgramLearningActivity programlearningactivity = context.ProgramLearningActivities.Find(id);
            if (programlearningactivity == null)
            {
                return HttpNotFound();
            }

            PopulateAssignedCompetencyData(programlearningactivity);
            return View(programlearningactivity);
        }


        private void PopulateAssignedCompetencyData(ProgramLearningActivity programlearningactivity)
        {
            var allCompetencies = context.Competencies;
            var instructorCourses = new HashSet<int>(programlearningactivity.ProgramLearningActivitiesCompetency.Select(c => c.CompetenciesID));

            var viewModel = new List<AssignedCompetencyData>();
            foreach (var competencie in allCompetencies)
            {
                viewModel.Add(new AssignedCompetencyData
                {
                    CompetenciesID = competencie.CompetenciesID,
                    Description = competencie.Description,
                    Assigned = instructorCourses.Contains(competencie.CompetenciesID)
                });
            }
            ViewBag.Competencys = viewModel;
        }
        //
        // GET: /ProgramCompetecies/Create

        public ActionResult Create()
        {
            ViewBag.ProgramLearningActivitiesID = new SelectList(context.ProgramLearningActivities, "ProgramLearningActivitiesID", "ProgramLearningActivitiesID");
            return View();
        }

        //
        // POST: /ProgramCompetecies/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProgramLearningActivitiesCompetency programlearningactivitiescompetency)
        {
            if (ModelState.IsValid)
            {
                context.ProgramLearningActivitiesCompetency.Add(programlearningactivitiescompetency);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProgramLearningActivitiesID = new SelectList(context.ProgramLearningActivities, "ProgramLearningActivitiesID", "ProgramLearningActivitiesID", programlearningactivitiescompetency.ProgramLearningActivitiesID);
            return View(programlearningactivitiescompetency);
        }

        //
        // GET: /ProgramLearningActivities/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ProgramLearningActivity programlearningactivity = context.ProgramLearningActivities.Find(id);


            if (programlearningactivity == null)
            {
                return HttpNotFound();
            }



            PopulateAssignedCompetencyData(programlearningactivity);

            ViewBag.LearningActivitiesID = new SelectList(context.LearningActivities, "LearningActivitiesID", "Description", programlearningactivity.LearningActivitiesID);
            ViewBag.ProgramID = new SelectList(context.Programs, "ProgramID", "Description", programlearningactivity.ProgramID);
            return View(programlearningactivity);
        }

        //
        // POST: /ProgramLearningActivities/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedCompetencys)
        {
            //                

            //1- Get fresh data from database
            var existingCompetenciesDB = context.ProgramLearningActivities
                .AsNoTracking()
                .Include(s => s.ProgramLearningActivitiesCompetency)
                .Where(s => s.ProgramLearningActivitiesID == id)
                .FirstOrDefault<ProgramLearningActivity>();

            if (ModelState.IsValid)
            {

                UpdateProgramLearningCompetencysDisconnected(selectedCompetencys, existingCompetenciesDB);


                /////context.Entry(existingCompetenciesDB).State = EntityState.Modified;
                /////context.SaveChanges();

                return RedirectToAction("Index");
            }

            //PopulateAssignedCourseData(instructorToUpdate);
            return View(existingCompetenciesDB);

        }

        private void UpdateProgramLearningCompetencysDisconnected(string[] selectedCompetencies, ProgramLearningActivity PrgramLearnigActivityToUpdate)
        {
            if (selectedCompetencies == null)
            {
                PrgramLearnigActivityToUpdate.ProgramLearningActivitiesCompetency = new List<ProgramLearningActivitiesCompetency>();
                return;
            }


            using (var context = new UCTContext())
            {
                // Do my save code here...



                List<ProgramLearningActivitiesCompetency> existingCompetencies = new List<ProgramLearningActivitiesCompetency>();
                existingCompetencies = PrgramLearnigActivityToUpdate.ProgramLearningActivitiesCompetency.ToList<ProgramLearningActivitiesCompetency>();


                var updatedCompetencys = new List<ProgramLearningActivitiesCompetency>();

                //populate all the competency checkboxes
                foreach (string competencyID in selectedCompetencies)
                {

                    updatedCompetencys.Add(new ProgramLearningActivitiesCompetency
                    {

                        ProgramLearningActivitiesID = PrgramLearnigActivityToUpdate.ProgramLearningActivitiesID,
                        CompetenciesID = int.Parse(competencyID),
                        LearningActivitiesID = (int)PrgramLearnigActivityToUpdate.LearningActivitiesID,
                        ProgramID = (int)PrgramLearnigActivityToUpdate.ProgramID,
                        LearningYear = 2003, //hardcoded for now


                    });

                }


                //2- Find newly added competecies 
                //by updatedCompetency (competencys came from client sided) - existingCompetencies = newly added Competencies
                var addedCompetcies = updatedCompetencys.Except(existingCompetencies, competency => competency.CompetenciesID);


                //3- Find deleted teachers 
                //by existing teachers - updatedTeachers = deleted teachers
                var deletedCompetencies = existingCompetencies.Except(updatedCompetencys, competency => competency.CompetenciesID);

                //4- Find modified teachers 
                //by updatedTeachers - addedTeachers = modified teachers
                var modifiedCompetencies = updatedCompetencys.Except(addedCompetcies, competency => competency.CompetenciesID);

                //4.1 sammit populate the id for the updatedComptencys
                modifiedCompetencies.ToList<ProgramLearningActivitiesCompetency>().ForEach(competecy =>
                    competecy.ID = existingCompetencies.Where(p => p.CompetenciesID == competecy.CompetenciesID).FirstOrDefault().ID
                    );

                //5- Mark all added teachers entity state to Added
                addedCompetcies.ToList<ProgramLearningActivitiesCompetency>().ForEach(competency => context.Entry(competency).State = EntityState.Added);

                //6- Mark all deleted teacher entity state to Deleted
                deletedCompetencies.ToList<ProgramLearningActivitiesCompetency>().ForEach(competency => context.Entry(competency).State = EntityState.Deleted);


                //7- Apply modified teachers current property values to existing property values
                foreach (ProgramLearningActivitiesCompetency teacher in modifiedCompetencies)
                {
                    //8- Find existing teacher by id from fresh database teachers
                    var existingTeacher = context.ProgramLearningActivitiesCompetency.Find(
                        teacher.ID
                        );

                    if (existingTeacher != null)
                    {
                        //9- Get DBEntityEntry object for each existing teacher entity
                        var teacherEntry = context.Entry(existingTeacher);
                        //10- overwrite all property current values from modified teachers' entity values, 
                        //so that it will have all modified values and mark entity as modified
                        teacherEntry.CurrentValues.SetValues(teacher);
                    }

                }


                //11- Save all above changed entities to the database
                context.SaveChanges();
            }

        }

        //
        // GET: /ProgramCompetecies/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ProgramLearningActivitiesCompetency programlearningactivitiescompetency = context.ProgramLearningActivitiesCompetency.Find(id);
            if (programlearningactivitiescompetency == null)
            {
                return HttpNotFound();
            }
            return View(programlearningactivitiescompetency);
        }

        //
        // POST: /ProgramCompetecies/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProgramLearningActivitiesCompetency programlearningactivitiescompetency = context.ProgramLearningActivitiesCompetency.Find(id);
            context.ProgramLearningActivitiesCompetency.Remove(programlearningactivitiescompetency);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
            base.Dispose(disposing);
        }


        private void UpdateProgramLearningCompetencys(string[] selectedCompetencies, ProgramLearningActivity PrgramLearnigActivityToUpdate)
        {
            if (selectedCompetencies == null)
            {
                PrgramLearnigActivityToUpdate.ProgramLearningActivitiesCompetency = new List<ProgramLearningActivitiesCompetency>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCompetencies);
            var instructorCourses = new HashSet<int>
                (PrgramLearnigActivityToUpdate.ProgramLearningActivitiesCompetency.Select(c => c.CompetenciesID));
            foreach (var course in context.ProgramLearningActivitiesCompetency)
            {
                if (selectedCoursesHS.Contains(course.CompetenciesID.ToString()))
                {
                    if (!instructorCourses.Contains(course.CompetenciesID))
                    {
                        PrgramLearnigActivityToUpdate.ProgramLearningActivitiesCompetency.Add(new ProgramLearningActivitiesCompetency
                        {

                            ProgramLearningActivitiesID = PrgramLearnigActivityToUpdate.ProgramLearningActivitiesID,
                            CompetenciesID = course.CompetenciesID,
                            LearningActivitiesID = (int)PrgramLearnigActivityToUpdate.LearningActivitiesID,
                            ProgramID = (int)PrgramLearnigActivityToUpdate.ProgramID,
                            LearningYear = 2003, //hardcoded for now


                        });
                    }
                }
                else
                {
                    if (instructorCourses.Contains(course.CompetenciesID))
                    {
                        PrgramLearnigActivityToUpdate.ProgramLearningActivitiesCompetency.Remove(course);
                    }
                }
            }
        }

    }
}