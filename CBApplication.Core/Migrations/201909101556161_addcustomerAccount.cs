namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcustomerAccount : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CustomerAccounts", "AccountNumber", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CustomerAccounts", "AccountNumber", c => c.String(nullable: false));
        }
    }
}
