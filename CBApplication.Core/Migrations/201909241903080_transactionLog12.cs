namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class transactionLog12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransactionsLogs", "MainAccountCategory_Id", c => c.Int());
            CreateIndex("dbo.TransactionsLogs", "MainAccountCategory_Id");
            AddForeignKey("dbo.TransactionsLogs", "MainAccountCategory_Id", "dbo.MainAccountCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionsLogs", "MainAccountCategory_Id", "dbo.MainAccountCategories");
            DropIndex("dbo.TransactionsLogs", new[] { "MainAccountCategory_Id" });
            DropColumn("dbo.TransactionsLogs", "MainAccountCategory_Id");
        }
    }
}
