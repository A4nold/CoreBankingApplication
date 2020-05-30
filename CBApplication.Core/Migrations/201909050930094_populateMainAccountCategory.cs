namespace CBApplication.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populateMainAccountCategory : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO MainAccountCategories (AcctAssetName) VALUES ('Asset')");
            Sql("INSERT INTO MainAccountCategories (AcctAssetName) VALUES ('Capital')");
            Sql("INSERT INTO MainAccountCategories (AcctAssetName) VALUES ('Liability')");
            Sql("INSERT INTO MainAccountCategories (AcctAssetName) VALUES ('Expenses')");
            Sql("INSERT INTO MainAccountCategories (AcctAssetName) VALUES ('Income')");
        }

        public override void Down()
        {
        }
    }
}
