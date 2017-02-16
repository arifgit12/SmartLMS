namespace SmartLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fileNameadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Description", c => c.String(nullable: false));
            AddColumn("dbo.Courses", "StartDate", c => c.DateTime());
            AddColumn("dbo.StudentCourses", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.Assignments", "FileName", c => c.String(maxLength: 255));
            AddColumn("dbo.Lectures", "FileName", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lectures", "FileName");
            DropColumn("dbo.Assignments", "FileName");
            DropColumn("dbo.StudentCourses", "Status");
            DropColumn("dbo.Courses", "StartDate");
            DropColumn("dbo.Courses", "Description");
        }
    }
}
