namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loanMigration1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Loans", "EndDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Loans", "EndDate");
        }
    }
}
