namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loanMigration3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Loans", "CustomerAccountNumber", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Loans", "CustomerAccountNumber");
        }
    }
}
