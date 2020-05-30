namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GlAccount1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GlAccountManagements", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.GlAccountManagements", "AccountAssigned", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GlAccountManagements", "AccountAssigned", c => c.Boolean(nullable: false));
            DropColumn("dbo.GlAccountManagements", "Name");
        }
    }
}
