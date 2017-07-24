using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Assigner.Models.Utils;

namespace Assigner.Models.CoreEntities.Utils
{
    [Table("RankStudent")]
    public class RankStudent
    {
        [Display(Name = "Student Rank ID")]
        public int ID { get; set; }

        [Column("RankTitle")]
        [Display(Name = "Student Rank")]
        [Index(IsUnique = true)]
        public StudentRanks Rank { get; set; }

        [Display(Name = "Student Rank")]
        [ScaffoldColumn(false)]
        public string RankString
        {
            get
            {
                switch (Rank)
                {
                    case StudentRanks.Undergraduate:
                        return "Under Graduate";
                    case StudentRanks.Graduate:
                        return "Graduate";
                    case StudentRanks.Postgraduate:
                        return "Post Graduate";
                    default:
                        throw new ArgumentOutOfRangeException($"The specified value {Rank} of Rank is not within acceptable values of the enum");
                }
            }
        }
    }
}