namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bankConfigEdit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankConfigs", "DayCount", c => c.Int(nullable: false));
            AddColumn("dbo.BankConfigs", "MonthCount", c => c.Int(nullable: false));
            AddColumn("dbo.BankConfigs", "YearCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BankConfigs", "YearCount");
            DropColumn("dbo.BankConfigs", "MonthCount");
            DropColumn("dbo.BankConfigs", "DayCount");
        }
    }
}
