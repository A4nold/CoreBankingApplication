namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loan24 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Loans", "LoanDialyInterestAccrued", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Loans", "LoanDialyInterestAccrued");
        }
    }
}
