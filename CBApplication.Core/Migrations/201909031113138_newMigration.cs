namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BranchName = c.String(nullable: false, maxLength: 225),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 225),
                        Address = c.String(nullable: false, maxLength: 225),
                        Gender = c.String(nullable: false, maxLength: 225),
                        PhoneNumber = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 225),
                        FullName = c.String(nullable: false, maxLength: 225),
                        Branch_Id = c.Byte(nullable: false),
                        Username = c.String(nullable: false, maxLength: 225),
                        PhoneNumber = c.String(nullable: false, maxLength: 15),
                        Password = c.String(),
                        PassChanged = c.Boolean(nullable: false),
                        Role = c.String(nullable: false),
                        Branch_Id1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.Branch_Id1, cascadeDelete: true)
                .Index(t => t.Branch_Id1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Branch_Id1", "dbo.Branches");
            DropIndex("dbo.Users", new[] { "Branch_Id1" });
            DropTable("dbo.Users");
            DropTable("dbo.Customers");
            DropTable("dbo.Branches");
        }
    }
}
