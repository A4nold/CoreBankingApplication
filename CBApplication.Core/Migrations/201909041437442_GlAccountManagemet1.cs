namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GlAccountManagemet1 : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GlAccountManagements");
        }
    }
}
