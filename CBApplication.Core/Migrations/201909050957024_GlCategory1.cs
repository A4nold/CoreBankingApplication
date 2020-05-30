namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GlCategory1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GlCategoryManagements", "MainAccountCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.GlCategoryManagements", "MainAccountCategoryId");
            AddForeignKey("dbo.GlCategoryManagements", "MainAccountCategoryId", "dbo.MainAccountCategories", "Id", cascadeDelete: true);
            DropColumn("dbo.GlCategoryManagements", "mainAccountCategory");
            DropColumn("dbo.GlCategoryManagements", "subAccountCategory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GlCategoryManagements", "subAccountCategory", c => c.String());
            AddColumn("dbo.GlCategoryManagements", "mainAccountCategory", c => c.String(nullable: false));
            DropForeignKey("dbo.GlCategoryManagements", "MainAccountCategoryId", "dbo.MainAccountCategories");
            DropIndex("dbo.GlCategoryManagements", new[] { "MainAccountCategoryId" });
            DropColumn("dbo.GlCategoryManagements", "MainAccountCategoryId");
        }
    }
}
