namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editbranch : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Branch_Id1", "dbo.Branches");
            DropIndex("dbo.Users", new[] { "Branch_Id1" });
            AlterColumn("dbo.Users", "Branch_Id1", c => c.Int());
            CreateIndex("dbo.Users", "Branch_Id1");
            AddForeignKey("dbo.Users", "Branch_Id1", "dbo.Branches", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Branch_Id1", "dbo.Branches");
            DropIndex("dbo.Users", new[] { "Branch_Id1" });
            AlterColumn("dbo.Users", "Branch_Id1", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "Branch_Id1");
            AddForeignKey("dbo.Users", "Branch_Id1", "dbo.Branches", "Id", cascadeDelete: true);
        }
    }
}
