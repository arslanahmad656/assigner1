namespace Assigner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeacherAddedToRegisterViewModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teachers", "ApplicationUserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Teachers", "ApplicationUserID", unique: true);
            AddForeignKey("dbo.Teachers", "ApplicationUserID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachers", "ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Teachers", new[] { "ApplicationUserID" });
            DropColumn("dbo.Teachers", "ApplicationUserID");
        }
    }
}
