namespace Assigner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoCascadedForeignKeysDetected : DbMigration
    {
        public override void Up()
        {
            // For teachers
            DropForeignKey("dbo.Teachers", "ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Teachers", new[] { "ApplicationUserID" });
            DropColumn("dbo.Teachers", "ApplicationUserID");

            // For students
            DropForeignKey("dbo.Student", "ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Student", new[] { "ApplicationUserID" });
            DropColumn("dbo.Student", "ApplicationUserID");

            // For teachers
            AddColumn("dbo.Teachers", "ApplicationUserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Teachers", "ApplicationUserID", unique: true);
            AddForeignKey("dbo.Teachers", "ApplicationUserID", "dbo.AspNetUsers", "Id", cascadeDelete: true);

            // For students
            AddColumn("dbo.Student", "ApplicationUserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Student", "ApplicationUserID", unique: true);
            AddForeignKey("dbo.Student", "ApplicationUserID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }

        public override void Down()
        {
            // For teachers
            DropForeignKey("dbo.Teachers", "ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Teachers", new[] { "ApplicationUserID" });
            DropColumn("dbo.Teachers", "ApplicationUserID");

            // For students
            DropForeignKey("dbo.Student", "ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Student", new[] { "ApplicationUserID" });
            DropColumn("dbo.Student", "ApplicationUserID");

            // For teachers
            AddColumn("dbo.Teachers", "ApplicationUserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Teachers", "ApplicationUserID", unique: true);
            AddForeignKey("dbo.Teachers", "ApplicationUserID", "dbo.AspNetUsers", "Id");

            // For students
            AddColumn("dbo.Student", "ApplicationUserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Student", "ApplicationUserID", unique: true);
            AddForeignKey("dbo.Student", "ApplicationUserID", "dbo.AspNetUsers", "Id");
        }
    }
}
