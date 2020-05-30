namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GlAccount : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GlAccountManagements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryManagementId = c.String(nullable: false),
                        BranchId = c.String(nullable: false),
                        AccCode = c.String(maxLength: 45),
                        AccountAssigned = c.Boolean(nullable: false),
                        AccountBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Branch_Id = c.Int(),
                        CategoryManagement_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.Branch_Id)
                .ForeignKey("dbo.GlCategoryManagements", t => t.CategoryManagement_Id)
                .Index(t => t.Branch_Id)
                .Index(t => t.CategoryManagement_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GlAccountManagements", "CategoryManagement_Id", "dbo.GlCategoryManagements");
            DropForeignKey("dbo.GlAccountManagements", "Branch_Id", "dbo.Branches");
            DropIndex("dbo.GlAccountManagements", new[] { "CategoryManagement_Id" });
            DropIndex("dbo.GlAccountManagements", new[] { "Branch_Id" });
            DropTable("dbo.GlAccountManagements");
        }
    }
}
