using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Assigner.Models.Utils;
using Assigner.Models.CoreEntities;
using Assigner.Models.CoreEntities.Utils;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Assigner.Models.Database
{
    public class AssignerContext : IdentityDbContext<ApplicationUser>
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public AssignerContext() : base("name=AssignerContext")
        {
        }

        public DbSet<Major> Majors { get; set; }

        public DbSet<RankStudent> RankStudents { get; set; }

        public DbSet<RankTeacher> RankTeachers { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Project> Projects { get; set; }

    }
}
