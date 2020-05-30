namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GlAccount2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GlAccountManagements", "AccCode", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GlAccountManagements", "AccCode", c => c.String(maxLength: 45));
        }
    }
}
