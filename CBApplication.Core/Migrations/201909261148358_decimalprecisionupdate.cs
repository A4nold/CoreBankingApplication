namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class decimalprecisionupdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.COTPosts", "Amount", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.CustomerAccounts", "CurrentLien", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.CustomerAccounts", "InterestGot", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.CustomerAccounts", "Balance", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.CustomerAccounts", "CotAccured", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.GlAccountManagements", "AccountBalance", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.CurrentConfigs", "CreditInterestRate", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.CurrentConfigs", "MinBalance", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.CurrentConfigs", "CommissionOnTurnover", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.GlPostings", "Amount", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.LoanConfigs", "DebitInterestRate", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.Loans", "Amount", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.Loans", "Interest", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.Loans", "InterestRate", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.Loans", "DailyLoanDeduction", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.Loans", "InterestDeduction", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.Loans", "LoanDialyInterestAccrued", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.Loans", "LoanBalance", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.Loans", "PrincipalRemaining", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.Loans", "InterestRemaining", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.SavingsConfigs", "CreditInterestRate", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.SavingsConfigs", "MinBalance", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.TellerPostings", "Amount", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.TransactionsLogs", "Amount", c => c.Decimal(nullable: false, precision: 20, scale: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TransactionsLogs", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.TellerPostings", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.SavingsConfigs", "MinBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.SavingsConfigs", "CreditInterestRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Loans", "InterestRemaining", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Loans", "PrincipalRemaining", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Loans", "LoanBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Loans", "LoanDialyInterestAccrued", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Loans", "InterestDeduction", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Loans", "DailyLoanDeduction", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Loans", "InterestRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Loans", "Interest", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Loans", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.LoanConfigs", "DebitInterestRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.GlPostings", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.CurrentConfigs", "CommissionOnTurnover", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.CurrentConfigs", "MinBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.CurrentConfigs", "CreditInterestRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.GlAccountManagements", "AccountBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.CustomerAccounts", "CotAccured", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.CustomerAccounts", "Balance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.CustomerAccounts", "InterestGot", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.CustomerAccounts", "CurrentLien", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.COTPosts", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
