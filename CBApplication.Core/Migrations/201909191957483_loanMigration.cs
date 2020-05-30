namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loanMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Loans", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Loans", "Name");
        }
    }
}
