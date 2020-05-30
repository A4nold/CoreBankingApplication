namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mainAccountCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MainAccountCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AcctAssetName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MainAccountCategories");
        }
    }
}
