namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class glaccountupdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GlAccountManagements", "AccountAssigned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GlAccountManagements", "AccountAssigned", c => c.String());
        }
    }
}
