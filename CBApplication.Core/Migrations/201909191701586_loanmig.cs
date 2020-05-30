namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loanmig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Loans", "Interest", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Loans", "DailyLoanDeduction", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Loans", "InterestDeduction", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Loans", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Loans", "PrincipalRemaining", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Loans", "InterestRemaining", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Loans", "EndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Loans", "EndDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Loans", "InterestRemaining");
            DropColumn("dbo.Loans", "PrincipalRemaining");
            DropColumn("dbo.Loans", "Status");
            DropColumn("dbo.Loans", "InterestDeduction");
            DropColumn("dbo.Loans", "DailyLoanDeduction");
            DropColumn("dbo.Loans", "Interest");
        }
    }
}
