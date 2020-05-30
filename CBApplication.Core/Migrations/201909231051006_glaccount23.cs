namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class glaccount23 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerAccounts", "CotAccured", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerAccounts", "CotAccured");
        }
    }
}
