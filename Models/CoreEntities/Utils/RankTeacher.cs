using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Assigner.Models.Utils;

namespace Assigner.Models.CoreEntities.Utils
{
    public class RankTeacher
    {
        [Display(Name = "Teacher Rank ID")]
        public int ID { get; set; }

        [Display(Name = "Teacher Rank")]
        [Column("RankTitle")]
        [Index(IsUnique = true)]
        public TeacherRanks Rank { get; set; }

        [Display(Name = "Student Rank")]
        [ScaffoldColumn(false)]
        public string RankString
        {
            get
            {
                switch (Rank)
                {
                    case TeacherRanks.Graduate:
                        return "Graduate";
                    case TeacherRanks.Postgraduate:
                        return "Post Graduate";
                    default:
                        throw new ArgumentOutOfRangeException($"The specified value {Rank} of Rank is not within acceptable values of the enum");
                }
            }
        }
    }
}