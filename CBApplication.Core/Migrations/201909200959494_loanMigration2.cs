namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loanMigration2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Loans", "AccountNumber", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Loans", "AccountNumber");
        }
    }
}
