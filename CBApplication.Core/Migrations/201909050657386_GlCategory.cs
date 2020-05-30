namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GlCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GlCategoryManagements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        Description = c.String(nullable: false, maxLength: 225),
                        mainAccountCategory = c.String(nullable: false),
                        subAccountCategory = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.GlAccountManagements");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GlAccountManagements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        Description = c.String(nullable: false, maxLength: 225),
                        mainAccountCategory = c.String(nullable: false),
                        SubAccountCategory = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.GlCategoryManagements");
        }
    }
}
