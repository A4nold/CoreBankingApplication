namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class savingsAccount3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SavingsAccounts", "MinBalance", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SavingsAccounts", "MinBalance");
        }
    }
}
