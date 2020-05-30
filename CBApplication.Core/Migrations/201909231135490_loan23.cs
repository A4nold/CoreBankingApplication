namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loan23 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoanConfigs", "LoanInterestReceivableGlId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LoanConfigs", "LoanInterestReceivableGlId");
        }
    }
}
