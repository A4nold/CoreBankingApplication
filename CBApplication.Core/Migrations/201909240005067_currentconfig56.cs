namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class currentconfig56 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CurrentConfigs", "CurrentInterestPayableGlId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CurrentConfigs", "CurrentInterestPayableGlId");
        }
    }
}
