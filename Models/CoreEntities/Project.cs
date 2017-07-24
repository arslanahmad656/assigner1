using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Assigner.Models.CoreEntities.Utils;

namespace Assigner.Models.CoreEntities
{
    [Table("Project")]
    public class Project
    {
        [Display(Name = "Project ID")]
        public int ID { get; set; }

        [Required]
        //[Index(IsUnique = true)] Do it using Fluent API
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Project Creation Time")]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        // Causes cyclic foreign key constraints
        //[Required]
        //[Display(Name = "Major ID")]
        //public int MajorID { get; set; }

        [Required]
        [Display(Name = "Rank ID")]
        public int RankID { get; set; }

        [Required]
        [Display(Name = "Teacher ID")]
        public int TeacherID { get; set; }

        //[Display(Name = "Major")]
        //public virtual Major Major { get; set; }

        [Display(Name = "Rank")]
        public virtual RankStudent Rank { get; set; }

        [Display(Name = "Teacher")]
        public virtual Teacher Teacher { get; set; }
    }
}