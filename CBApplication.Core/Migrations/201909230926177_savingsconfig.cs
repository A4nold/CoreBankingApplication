namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class savingsconfig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SavingsConfigs", "SavingsInterestPayableGlId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SavingsConfigs", "SavingsInterestPayableGlId");
        }
    }
}
