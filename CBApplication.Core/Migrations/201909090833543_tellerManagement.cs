namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tellerManagement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TellerManagements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        userId = c.Int(nullable: false),
                        CategoryManagementId = c.Int(nullable: false),
                        Date = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GlCategoryManagements", t => t.CategoryManagementId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId)
                .Index(t => t.CategoryManagementId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TellerManagements", "userId", "dbo.Users");
            DropForeignKey("dbo.TellerManagements", "CategoryManagementId", "dbo.GlCategoryManagements");
            DropIndex("dbo.TellerManagements", new[] { "CategoryManagementId" });
            DropIndex("dbo.TellerManagements", new[] { "userId" });
            DropTable("dbo.TellerManagements");
        }
    }
}
