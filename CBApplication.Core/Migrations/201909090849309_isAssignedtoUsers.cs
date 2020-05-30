namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isAssignedtoUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserAssigned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "UserAssigned");
        }
    }
}
