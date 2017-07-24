using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assigner.Models.CoreEntities.BridgeEntities
{
    using CoreEntities;

    public class StudentChosenProject
    {
        [Display(Name = "Student Chosen Project Id")]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Student ID")]
        public int StudentID { get; set; }

        [Required]
        [Display(Name = "Project ID")]
        public int ProjectID { get; set; }

        [Required]
        [Display(Name = "Submission Status")]
        public bool Submitted { get; set; }

        [Display(Name = "Student")]
        public virtual Student Student { get; set; }

        [Display(Name = "Project")]
        public virtual Project Project { get; set; }
    }
}