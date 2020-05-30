namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loanConfig1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoanConfigs", "IncomeGlAccountId", c => c.Int(nullable: false));
            AddColumn("dbo.LoanConfigs", "Status", c => c.Boolean(nullable: false));
            DropColumn("dbo.LoanConfigs", "ExpenseGlAccountId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LoanConfigs", "ExpenseGlAccountId", c => c.Int(nullable: false));
            DropColumn("dbo.LoanConfigs", "Status");
            DropColumn("dbo.LoanConfigs", "IncomeGlAccountId");
        }
    }
}
