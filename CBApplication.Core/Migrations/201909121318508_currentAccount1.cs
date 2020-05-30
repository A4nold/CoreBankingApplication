namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class currentAccount1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CurrentAccounts", "GlAccountId", "dbo.GlAccountManagements");
            DropIndex("dbo.CurrentAccounts", new[] { "GlAccountId" });
            RenameColumn(table: "dbo.CurrentAccounts", name: "GlAccountId", newName: "GlAccount_Id");
            AddColumn("dbo.CurrentAccounts", "ExpenseGlAccountId", c => c.Int(nullable: false));
            AddColumn("dbo.CurrentAccounts", "IncomeGlAccountId", c => c.Int(nullable: false));
            AlterColumn("dbo.CurrentAccounts", "GlAccount_Id", c => c.Int());
            CreateIndex("dbo.CurrentAccounts", "GlAccount_Id");
            AddForeignKey("dbo.CurrentAccounts", "GlAccount_Id", "dbo.GlAccountManagements", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CurrentAccounts", "GlAccount_Id", "dbo.GlAccountManagements");
            DropIndex("dbo.CurrentAccounts", new[] { "GlAccount_Id" });
            AlterColumn("dbo.CurrentAccounts", "GlAccount_Id", c => c.Int(nullable: false));
            DropColumn("dbo.CurrentAccounts", "IncomeGlAccountId");
            DropColumn("dbo.CurrentAccounts", "ExpenseGlAccountId");
            RenameColumn(table: "dbo.CurrentAccounts", name: "GlAccount_Id", newName: "GlAccountId");
            CreateIndex("dbo.CurrentAccounts", "GlAccountId");
            AddForeignKey("dbo.CurrentAccounts", "GlAccountId", "dbo.GlAccountManagements", "Id", cascadeDelete: true);
        }
    }
}
