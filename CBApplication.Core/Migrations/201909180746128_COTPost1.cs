namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class COTPost1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.COTPosts", name: "AccountToCreditId", newName: "GlAccountId");
            RenameColumn(table: "dbo.COTPosts", name: "AccountToDebitId", newName: "CustomerAccountId");
            RenameIndex(table: "dbo.COTPosts", name: "IX_AccountToDebitId", newName: "IX_CustomerAccountId");
            RenameIndex(table: "dbo.COTPosts", name: "IX_AccountToCreditId", newName: "IX_GlAccountId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.COTPosts", name: "IX_GlAccountId", newName: "IX_AccountToCreditId");
            RenameIndex(table: "dbo.COTPosts", name: "IX_CustomerAccountId", newName: "IX_AccountToDebitId");
            RenameColumn(table: "dbo.COTPosts", name: "CustomerAccountId", newName: "AccountToDebitId");
            RenameColumn(table: "dbo.COTPosts", name: "GlAccountId", newName: "AccountToCreditId");
        }
    }
}
