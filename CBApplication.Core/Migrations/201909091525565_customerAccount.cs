namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customerAccount : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        customerId = c.Int(nullable: false),
                        branchId = c.Int(nullable: false),
                        AccountNumber = c.String(nullable: false),
                        AccountName = c.String(nullable: false),
                        isClosed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.branchId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.customerId, cascadeDelete: true)
                .Index(t => t.customerId)
                .Index(t => t.branchId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerAccounts", "customerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerAccounts", "branchId", "dbo.Branches");
            DropIndex("dbo.CustomerAccounts", new[] { "branchId" });
            DropIndex("dbo.CustomerAccounts", new[] { "customerId" });
            DropTable("dbo.CustomerAccounts");
        }
    }
}
