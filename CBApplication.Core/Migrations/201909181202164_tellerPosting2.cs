namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tellerPosting2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TellerPostings", "UserId", c => c.Int());
            CreateIndex("dbo.TellerPostings", "UserId");
            AddForeignKey("dbo.TellerPostings", "UserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TellerPostings", "UserId", "dbo.Users");
            DropIndex("dbo.TellerPostings", new[] { "UserId" });
            DropColumn("dbo.TellerPostings", "UserId");
        }
    }
}
