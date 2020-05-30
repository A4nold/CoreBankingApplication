namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customerAccount3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerAccounts", "branchId", "dbo.Branches");
            DropIndex("dbo.CustomerAccounts", new[] { "branchId" });
            RenameColumn(table: "dbo.CustomerAccounts", name: "branchId", newName: "Branches_Id");
            AddColumn("dbo.CustomerAccounts", "branchName", c => c.String());
            AlterColumn("dbo.CustomerAccounts", "Branches_Id", c => c.Int());
            CreateIndex("dbo.CustomerAccounts", "Branches_Id");
            AddForeignKey("dbo.CustomerAccounts", "Branches_Id", "dbo.Branches", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerAccounts", "Branches_Id", "dbo.Branches");
            DropIndex("dbo.CustomerAccounts", new[] { "Branches_Id" });
            AlterColumn("dbo.CustomerAccounts", "Branches_Id", c => c.Int(nullable: false));
            DropColumn("dbo.CustomerAccounts", "branchName");
            RenameColumn(table: "dbo.CustomerAccounts", name: "Branches_Id", newName: "branchId");
            CreateIndex("dbo.CustomerAccounts", "branchId");
            AddForeignKey("dbo.CustomerAccounts", "branchId", "dbo.Branches", "Id", cascadeDelete: true);
        }
    }
}
