using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Assigner.Models.Utils;


namespace Assigner.Models.CoreEntities.Utils
{
    [Table("Majors")]
    public class Major
    {
        [Display(Name = "Majors ID")]
        public int ID { get; set; }

        [Column("MajorTitle")]
        [Display(Name = "Majors")]
        [Index(IsUnique = true)]
        public Majors Majors { get; set; }

        [Display(Name = "Majors")]
        [ScaffoldColumn(false)]
        public string MajorString
        {
            get
            {
                switch (Majors)
                {
                    case Majors.ComputerScience:
                        return "Computer Science";
                    case Majors.SoftwareEngineering:
                        return "Software Engineering";
                    case Majors.InformationTechnology:
                        return "Information Technology";
                    default:
                        throw new ArgumentOutOfRangeException($"The specified value {Majors} of Majors is not within acceptable values of the enum");
                }
            }
        }
    }
}