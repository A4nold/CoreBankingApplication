namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tellerPosting1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TellerPostings", "UserId", "dbo.Users");
            DropIndex("dbo.TellerPostings", new[] { "UserId" });
            AddColumn("dbo.TellerPostings", "Narration", c => c.String());
            DropColumn("dbo.TellerPostings", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TellerPostings", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.TellerPostings", "Narration");
            CreateIndex("dbo.TellerPostings", "UserId");
            AddForeignKey("dbo.TellerPostings", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
