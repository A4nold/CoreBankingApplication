namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class transactionLog14 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TransactionsLogs", "MainAccountCategory_Id", "dbo.MainAccountCategories");
            DropIndex("dbo.TransactionsLogs", new[] { "MainAccountCategory_Id" });
            AddColumn("dbo.TransactionsLogs", "MainAccountCategory", c => c.String());
            DropColumn("dbo.TransactionsLogs", "MainAccountCategory_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TransactionsLogs", "MainAccountCategory_Id", c => c.Int());
            DropColumn("dbo.TransactionsLogs", "MainAccountCategory");
            CreateIndex("dbo.TransactionsLogs", "MainAccountCategory_Id");
            AddForeignKey("dbo.TransactionsLogs", "MainAccountCategory_Id", "dbo.MainAccountCategories", "Id");
        }
    }
}
