namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anymigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerAccounts", "customerId", "dbo.Customers");
            DropIndex("dbo.CustomerAccounts", new[] { "customerId" });
            AddColumn("dbo.CustomerAccounts", "Customers_id", c => c.Int());
            AlterColumn("dbo.CustomerAccounts", "customerId", c => c.String(nullable: false));
            CreateIndex("dbo.CustomerAccounts", "Customers_id");
            AddForeignKey("dbo.CustomerAccounts", "Customers_id", "dbo.Customers", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerAccounts", "Customers_id", "dbo.Customers");
            DropIndex("dbo.CustomerAccounts", new[] { "Customers_id" });
            AlterColumn("dbo.CustomerAccounts", "customerId", c => c.Int(nullable: false));
            DropColumn("dbo.CustomerAccounts", "Customers_id");
            CreateIndex("dbo.CustomerAccounts", "customerId");
            AddForeignKey("dbo.CustomerAccounts", "customerId", "dbo.Customers", "id", cascadeDelete: true);
        }
    }
}
