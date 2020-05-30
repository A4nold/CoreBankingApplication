namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropglposting : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GlPostings", "CreditAccount_Id", "dbo.GlAccountManagements");
            DropForeignKey("dbo.GlPostings", "DebitAccount_Id", "dbo.GlAccountManagements");
            DropForeignKey("dbo.GlPostings", "UserId", "dbo.Users");
            DropIndex("dbo.GlPostings", new[] { "UserId" });
            DropIndex("dbo.GlPostings", new[] { "CreditAccount_Id" });
            DropIndex("dbo.GlPostings", new[] { "DebitAccount_Id" });
            DropTable("dbo.GlPostings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GlPostings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DebitAccountNarration = c.String(),
                        CreditAccountNarration = c.String(),
                        UserId = c.Int(nullable: false),
                        DatePosted = c.DateTime(nullable: false),
                        CreditAccount_Id = c.Int(),
                        DebitAccount_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.GlPostings", "DebitAccount_Id");
            CreateIndex("dbo.GlPostings", "CreditAccount_Id");
            CreateIndex("dbo.GlPostings", "UserId");
            AddForeignKey("dbo.GlPostings", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GlPostings", "DebitAccount_Id", "dbo.GlAccountManagements", "Id");
            AddForeignKey("dbo.GlPostings", "CreditAccount_Id", "dbo.GlAccountManagements", "Id");
        }
    }
}
