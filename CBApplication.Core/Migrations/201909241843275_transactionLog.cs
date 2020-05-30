namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class transactionLog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransactionsLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        date = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TransactionsLogs");
        }
    }
}
