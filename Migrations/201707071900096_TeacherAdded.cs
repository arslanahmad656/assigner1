namespace Assigner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeacherAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255, unicode: false),
                        Description = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        RankID = c.Int(nullable: false),
                        TeacherID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RankStudent", t => t.RankID, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherID, cascadeDelete: true)
                .Index(t => t.RankID)
                .Index(t => t.TeacherID);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        MajorID = c.Int(nullable: false),
                        RankID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Majors", t => t.MajorID, cascadeDelete: true)
                .ForeignKey("dbo.RankTeachers", t => t.RankID, cascadeDelete: true)
                .Index(t => t.MajorID)
                .Index(t => t.RankID);
            
            CreateTable(
                "dbo.RankTeachers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RankTitle = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.RankTitle, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Project", "TeacherID", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "RankID", "dbo.RankTeachers");
            DropForeignKey("dbo.Teachers", "MajorID", "dbo.Majors");
            DropForeignKey("dbo.Project", "RankID", "dbo.RankStudent");
            DropIndex("dbo.RankTeachers", new[] { "RankTitle" });
            DropIndex("dbo.Teachers", new[] { "RankID" });
            DropIndex("dbo.Teachers", new[] { "MajorID" });
            DropIndex("dbo.Project", new[] { "TeacherID" });
            DropIndex("dbo.Project", new[] { "RankID" });
            DropTable("dbo.RankTeachers");
            DropTable("dbo.Teachers");
            DropTable("dbo.Project");
        }
    }
}
