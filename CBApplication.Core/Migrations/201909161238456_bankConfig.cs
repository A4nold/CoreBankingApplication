namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bankConfig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankConfigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsOpen = c.Boolean(nullable: false),
                        FinancialDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BankConfigs");
        }
    }
}
