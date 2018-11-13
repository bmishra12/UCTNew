using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace UCT.Models
{
    public class Competency
    {

     

        public int CompetenciesID { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Competency Name cannot be longer than 10 characters.")]
        [Display(Name = "Competency")]
        public string Competency1 { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "Competency Description cannot be longer than 150 characters.")]
        public string Description { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
       
    }
}