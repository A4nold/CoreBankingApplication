namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class savingsAccount2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SavingsAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GlAccountId = c.Int(nullable: false),
                        CreditInterestRate = c.Double(nullable: false),
                        status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GlAccountManagements", t => t.GlAccountId, cascadeDelete: true)
                .Index(t => t.GlAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SavingsAccounts", "GlAccountId", "dbo.GlAccountManagements");
            DropIndex("dbo.SavingsAccounts", new[] { "GlAccountId" });
            DropTable("dbo.SavingsAccounts");
        }
    }
}
