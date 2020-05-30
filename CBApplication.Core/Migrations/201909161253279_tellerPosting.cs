namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tellerPosting : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TellerPostings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Type = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        DateOfPosting = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerAccounts", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TellerPostings", "UserId", "dbo.Users");
            DropForeignKey("dbo.TellerPostings", "CustomerId", "dbo.CustomerAccounts");
            DropIndex("dbo.TellerPostings", new[] { "UserId" });
            DropIndex("dbo.TellerPostings", new[] { "CustomerId" });
            DropTable("dbo.TellerPostings");
        }
    }
}
