namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class savingsAccount4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SavingsAccounts", "CreditInterestRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.SavingsAccounts", "MinBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SavingsAccounts", "MinBalance", c => c.Long(nullable: false));
            AlterColumn("dbo.SavingsAccounts", "CreditInterestRate", c => c.Double(nullable: false));
        }
    }
}
