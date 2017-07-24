using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Assigner.Models.CoreEntities.Utils;

namespace Assigner.Models.CoreEntities
{
    //[Table("Teacher")]
    public class Teacher
    {
        //public Teacher()
        //{
        //    ApplicationUserID = "id";
        //}

        [Display(Name = "Teacher ID")]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Major ID")]
        public int MajorID { get; set; }

        [Required]
        [Display(Name = "Rank ID")]
        public int RankID { get; set; }

        [Index(IsUnique = true)]
        [Display(Name = "Application User ID")]
        public string ApplicationUserID { get; set; }

        [Display(Name = "Application User")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Major")]
        public virtual Major Major { get; set; }

        [Display(Name = "Rank")]
        public virtual RankTeacher Rank { get; set; }
    }
}