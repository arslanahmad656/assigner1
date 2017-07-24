using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Assigner.Models.CoreEntities.BridgeEntities
{
    using CoreEntities;

    public class Submission
    {
        [Display(Name = "Submission ID")]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Student ID")]
        public int StudentID { get; set; }

        [Required]
        [Display(Name = "Project ID")]
        public int ProjectID { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Submission Date")]
        public DateTime SubmissionDate { get; set; }

        public virtual Student Student { get; set; }

        public virtual Project Project { get; set; }

    }
}