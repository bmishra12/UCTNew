using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace UCT.Models
{
    public class ProgramLearningActivity
    {
        public ProgramLearningActivity()
        {
            this.ProgramLearningActivitiesCompetency = new HashSet<ProgramLearningActivitiesCompetency>();
        }

        public int ProgramLearningActivitiesID { get; set; }
         [Required]
        public Nullable<int> ProgramID { get; set; }
         [Required]
        public Nullable<int> LearningActivitiesID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }

        public virtual LearningActivity LearningActivity { get; set; }
        public virtual Program Program { get; set; }
        public virtual ICollection<ProgramLearningActivitiesCompetency> ProgramLearningActivitiesCompetency { get; set; }

    }
}