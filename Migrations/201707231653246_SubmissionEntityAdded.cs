namespace Assigner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubmissionEntityAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Submissions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        ProjectID = c.Int(nullable: false),
                        SubmissionDate = c.DateTime(nullable: false),
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
            DropForeignKey("dbo.Submissions", "StudentID", "dbo.Student");
            DropForeignKey("dbo.Submissions", "ProjectID", "dbo.Project");
            DropIndex("dbo.Submissions", new[] { "ProjectID" });
            DropIndex("dbo.Submissions", new[] { "StudentID" });
            DropTable("dbo.Submissions");
        }
    }
}
