namespace Assigner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentChosenProjectEntityAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentChosenProjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        ProjectID = c.Int(nullable: false),
                        Submitted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                //.ForeignKey("dbo.Project", t => t.ProjectID, cascadeDelete: true)
                .ForeignKey("dbo.Project", t => t.ProjectID, cascadeDelete: false)
                //.ForeignKey("dbo.Student", t => t.StudentID, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.StudentID, cascadeDelete: false)
                .Index(t => t.StudentID)
                .Index(t => t.ProjectID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentChosenProjects", "StudentID", "dbo.Student");
            DropForeignKey("dbo.StudentChosenProjects", "ProjectID", "dbo.Project");
            DropIndex("dbo.StudentChosenProjects", new[] { "ProjectID" });
            DropIndex("dbo.StudentChosenProjects", new[] { "StudentID" });
            DropTable("dbo.StudentChosenProjects");
        }
    }
}
