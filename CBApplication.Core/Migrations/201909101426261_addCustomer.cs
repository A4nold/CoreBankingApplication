namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "customerId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "customerId");
        }
    }
}
