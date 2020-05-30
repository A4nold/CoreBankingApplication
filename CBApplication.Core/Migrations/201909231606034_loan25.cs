namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loan25 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Loans", "DaysCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Loans", "DaysCount");
        }
    }
}
