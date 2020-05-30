namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GlAccount3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GlAccountManagements", "MainAccountCategoryId", c => c.String());
            AddColumn("dbo.GlAccountManagements", "MainAccountCategory_Id", c => c.Int());
            CreateIndex("dbo.GlAccountManagements", "MainAccountCategory_Id");
            AddForeignKey("dbo.GlAccountManagements", "MainAccountCategory_Id", "dbo.MainAccountCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GlAccountManagements", "MainAccountCategory_Id", "dbo.MainAccountCategories");
            DropIndex("dbo.GlAccountManagements", new[] { "MainAccountCategory_Id" });
            DropColumn("dbo.GlAccountManagements", "MainAccountCategory_Id");
            DropColumn("dbo.GlAccountManagements", "MainAccountCategoryId");
        }
    }
}
