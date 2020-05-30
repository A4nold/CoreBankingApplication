namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateGlCategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GlCategoryManagements", "MainAccountCategoryId", "dbo.MainAccountCategories");
            DropIndex("dbo.GlCategoryManagements", new[] { "MainAccountCategoryId" });
            AddColumn("dbo.GlCategoryManagements", "MainAccountCategory_Id", c => c.Int());
            AlterColumn("dbo.GlCategoryManagements", "MainAccountCategoryId", c => c.String());
            CreateIndex("dbo.GlCategoryManagements", "MainAccountCategory_Id");
            AddForeignKey("dbo.GlCategoryManagements", "MainAccountCategory_Id", "dbo.MainAccountCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GlCategoryManagements", "MainAccountCategory_Id", "dbo.MainAccountCategories");
            DropIndex("dbo.GlCategoryManagements", new[] { "MainAccountCategory_Id" });
            AlterColumn("dbo.GlCategoryManagements", "MainAccountCategoryId", c => c.Int(nullable: false));
            DropColumn("dbo.GlCategoryManagements", "MainAccountCategory_Id");
            CreateIndex("dbo.GlCategoryManagements", "MainAccountCategoryId");
            AddForeignKey("dbo.GlCategoryManagements", "MainAccountCategoryId", "dbo.MainAccountCategories", "Id", cascadeDelete: true);
        }
    }
}
