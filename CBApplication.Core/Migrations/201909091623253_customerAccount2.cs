namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customerAccount2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerAccounts", "AccountType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerAccounts", "AccountType");
        }
    }
}
