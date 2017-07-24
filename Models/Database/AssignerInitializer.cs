using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Assigner.Models;
using Assigner.Models.Utils;
using Assigner.Models.CoreEntities;
using Assigner.Models.CoreEntities.Utils;

namespace Assigner.Models.Database
{
    public class AssignerInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            // Initializing Entities
            var majors = new List<Major>
            {
                 new Major
                 {
                     Majors = Majors.ComputerScience
                 },
                 new Major
                 {
                     Majors = Majors.SoftwareEngineering
                 },
                 new Major
                 {
                    Majors = Majors.InformationTechnology
                 }
            };

            var studentRanks = new List<RankStudent>
            {
                new RankStudent
                {
                    Rank = StudentRanks.Undergraduate
                },
                new RankStudent
                {
                    Rank = StudentRanks.Graduate
                },
                new RankStudent
                {
                    Rank = StudentRanks.Postgraduate
                }
            };

            var teacherRanks = new List<RankTeacher>
            {
                new RankTeacher
                {
                    Rank = TeacherRanks.Graduate
                },
                new RankTeacher
                {
                    Rank = TeacherRanks.Postgraduate
                }
            };

            var students = new List<Student>
            {
                new Student
                {
                    DateOfBirth = DateTime.Parse("01/01/1993"),
                    Major = majors[0],
                    Name = "Ernie Macmillan",
                    Rank = studentRanks[0]
                },
                new Student
                {
                    DateOfBirth = DateTime.Parse("01/02/1995"),
                    Major = majors[1],
                    Name = "Cedric Diggory",
                    Rank = studentRanks[1]
                },
                new Student
                {
                    DateOfBirth = DateTime.Parse("02/01/1997"),
                    Major = majors[2],
                    Name = "Pancy Parkinson",
                    Rank = studentRanks[2]
                }
            };

            var teachers = new List<Teacher>
            {
                new Teacher
                {
                    DateOfBirth = DateTime.Parse("07/09/1978"),
                    Major = majors[0],
                    Name = "Mad Eye Moody",
                    Rank = teacherRanks[0]
                },
                new Teacher
                {
                    DateOfBirth = DateTime.Parse("04/17/1984"),
                    Major = majors[1],
                    Name = "Sibyl Trelawny",
                    Rank = teacherRanks[1]
                },
                new Teacher
                {
                    DateOfBirth = DateTime.Parse("06/15/1988"),
                    Major = majors[2],
                    Name = "Gilderoy Lockhart",
                    Rank = teacherRanks[0]
                }
            };

            var projects = new List<Project>
            {
                new Project
                {
                    CreationDate = DateTime.Now.Subtract(new TimeSpan(122, 23, 24)),
                    Description = "This is the description of a project",
                    //Major = majors[0],
                    Rank = studentRanks[0],
                    Teacher = teachers[0],
                    Title = "A project"
                },
                new Project
                {
                    CreationDate = DateTime.Now.Subtract(new TimeSpan(224, 28, 22)),
                    Description = "This is the description of another project",
                    //Major = majors[1],
                    Rank = studentRanks[1],
                    Teacher = teachers[1],
                    Title = "Another project"
                },
                new Project
                {
                    CreationDate = DateTime.Now.Subtract(new TimeSpan(17, 3, 54)),
                    Description = "This is the description of still another project",
                    //Major = majors[0],
                    Rank = studentRanks[0],
                    Teacher = teachers[0],
                    Title = "Still another project"
                }
            };


            // Populating Db
            majors.ForEach(major => context.Majors.Add(major));
            studentRanks.ForEach(rank => context.RankStudents.Add(rank));
            teacherRanks.ForEach(rank => context.RankTeachers.Add(rank));
            students.ForEach(student => context.Students.Add(student));
            teachers.ForEach(teacher => context.Teachers.Add(teacher));
            projects.ForEach(project => context.Projects.Add(project));

            base.Seed(context);
        }
    }
}