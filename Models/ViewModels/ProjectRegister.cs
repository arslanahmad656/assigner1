using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Assigner.Models.ViewModels
{
    public class ProjectRegister
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Rank ID")]
        public int RankID { get; set; }
    }
}