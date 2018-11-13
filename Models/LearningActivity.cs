using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;


namespace UCT.Models
{
 
    public  class LearningActivity
    {
        public LearningActivity()
        {
            this.ProgramLearningActivities = new HashSet<ProgramLearningActivity>();
        }

        public int LearningActivitiesID { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "LearningActivity Description cannot be longer than 150 characters.")]
        public string Description { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }

        public virtual ICollection<ProgramLearningActivity> ProgramLearningActivities { get; set; }
    }
}
