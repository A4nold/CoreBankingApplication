namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tellerManagement1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TellerManagements", "CategoryManagementId", "dbo.GlCategoryManagements");
            DropIndex("dbo.TellerManagements", new[] { "CategoryManagementId" });
            AddColumn("dbo.TellerManagements", "AccountManagementId", c => c.Int(nullable: false));
            CreateIndex("dbo.TellerManagements", "AccountManagementId");
            AddForeignKey("dbo.TellerManagements", "AccountManagementId", "dbo.GlAccountManagements", "Id", cascadeDelete: true);
            DropColumn("dbo.TellerManagements", "CategoryManagementId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TellerManagements", "CategoryManagementId", c => c.Int(nullable: false));
            DropForeignKey("dbo.TellerManagements", "AccountManagementId", "dbo.GlAccountManagements");
            DropIndex("dbo.TellerManagements", new[] { "AccountManagementId" });
            DropColumn("dbo.TellerManagements", "AccountManagementId");
            CreateIndex("dbo.TellerManagements", "CategoryManagementId");
            AddForeignKey("dbo.TellerManagements", "CategoryManagementId", "dbo.GlCategoryManagements", "Id", cascadeDelete: true);
        }
    }
}
