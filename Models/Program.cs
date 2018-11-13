using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace UCT.Models
{
    public class Program
    {
        public Program()
        {
            this.ProgramLearningActivities = new HashSet<ProgramLearningActivity>();
        }

        public int ProgramID { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "Program Description cannot be longer than 150 characters.")]
        public string Description { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }

        public virtual ICollection<ProgramLearningActivity> ProgramLearningActivities { get; set; }
 
    }
}