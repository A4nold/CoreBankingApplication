namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class glposting : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GlPostings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DebitAccountNarration = c.String(),
                        DebitAccountId = c.Int(nullable: false),
                        CreditAccountNarration = c.String(),
                        CreditAccountId = c.Int(),
                        UserId = c.Int(),
                        DatePosted = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GlAccountManagements", t => t.CreditAccountId)
                .ForeignKey("dbo.GlAccountManagements", t => t.DebitAccountId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.DebitAccountId)
                .Index(t => t.CreditAccountId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GlPostings", "UserId", "dbo.Users");
            DropForeignKey("dbo.GlPostings", "DebitAccountId", "dbo.GlAccountManagements");
            DropForeignKey("dbo.GlPostings", "CreditAccountId", "dbo.GlAccountManagements");
            DropIndex("dbo.GlPostings", new[] { "UserId" });
            DropIndex("dbo.GlPostings", new[] { "CreditAccountId" });
            DropIndex("dbo.GlPostings", new[] { "DebitAccountId" });
            DropTable("dbo.GlPostings");
        }
    }
}
