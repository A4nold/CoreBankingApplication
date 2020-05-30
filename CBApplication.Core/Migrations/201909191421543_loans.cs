namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loans : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerAccountId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InterestRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Narration = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        DurationInMonths = c.Int(nullable: false),
                        RepaymentPlan = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerAccounts", t => t.CustomerAccountId, cascadeDelete: true)
                .Index(t => t.CustomerAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Loans", "CustomerAccountId", "dbo.CustomerAccounts");
            DropIndex("dbo.Loans", new[] { "CustomerAccountId" });
            DropTable("dbo.Loans");
        }
    }
}
