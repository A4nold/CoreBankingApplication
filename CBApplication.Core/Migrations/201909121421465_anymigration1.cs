namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anymigration1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CurrentAccounts", "status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CurrentAccounts", "status");
        }
    }
}
