namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogTransaction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransactionsLogs", "AccountCode", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TransactionsLogs", "AccountCode");
        }
    }
}
