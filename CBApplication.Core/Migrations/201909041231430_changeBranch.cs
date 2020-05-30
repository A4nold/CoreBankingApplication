namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeBranch : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "Branch_Id1" });
            DropColumn("dbo.Users", "Branch_Id");
            RenameColumn(table: "dbo.Users", name: "Branch_Id1", newName: "Branch_Id");
            AddColumn("dbo.Users", "Branch_Name", c => c.String());
            AlterColumn("dbo.Users", "Branch_Id", c => c.Int());
            CreateIndex("dbo.Users", "Branch_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Branch_Id" });
            AlterColumn("dbo.Users", "Branch_Id", c => c.Byte(nullable: false));
            DropColumn("dbo.Users", "Branch_Name");
            RenameColumn(table: "dbo.Users", name: "Branch_Id", newName: "Branch_Id1");
            AddColumn("dbo.Users", "Branch_Id", c => c.Byte(nullable: false));
            CreateIndex("dbo.Users", "Branch_Id1");
        }
    }
}
