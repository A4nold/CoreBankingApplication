namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerAccountLien : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerAccounts", "CurrentLien", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.CustomerAccounts", "InterestGot", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerAccounts", "InterestGot");
            DropColumn("dbo.CustomerAccounts", "CurrentLien");
        }
    }
}
