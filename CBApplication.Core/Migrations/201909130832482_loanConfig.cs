namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loanConfig : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CurrentAccounts", newName: "CurrentConfigs");
            RenameTable(name: "dbo.SavingsAccounts", newName: "SavingsConfigs");
            CreateTable(
                "dbo.LoanConfigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DebitInterestRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpenseGlAccountId = c.Int(nullable: false),
                        GlAccount_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GlAccountManagements", t => t.GlAccount_Id)
                .Index(t => t.GlAccount_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoanConfigs", "GlAccount_Id", "dbo.GlAccountManagements");
            DropIndex("dbo.LoanConfigs", new[] { "GlAccount_Id" });
            DropTable("dbo.LoanConfigs");
            RenameTable(name: "dbo.SavingsConfigs", newName: "SavingsAccounts");
            RenameTable(name: "dbo.CurrentConfigs", newName: "CurrentAccounts");
        }
    }
}
