namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class currentAccount : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CurrentAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreditInterestRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MinBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GlAccountId = c.Int(nullable: false),
                        CommissionOnTurnover = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CotGlAccount_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GlAccountManagements", t => t.CotGlAccount_Id)
                .ForeignKey("dbo.GlAccountManagements", t => t.GlAccountId, cascadeDelete: true)
                .Index(t => t.GlAccountId)
                .Index(t => t.CotGlAccount_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CurrentAccounts", "GlAccountId", "dbo.GlAccountManagements");
            DropForeignKey("dbo.CurrentAccounts", "CotGlAccount_Id", "dbo.GlAccountManagements");
            DropIndex("dbo.CurrentAccounts", new[] { "CotGlAccount_Id" });
            DropIndex("dbo.CurrentAccounts", new[] { "GlAccountId" });
            DropTable("dbo.CurrentAccounts");
        }
    }
}
