namespace Price_comparison_engine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Newer1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CommentsTables", newName: "CommentsTable");
            RenameTable(name: "dbo.ItemsTables", newName: "ItemsTable");
            RenameTable(name: "dbo.ShopRatingTables", newName: "ShopRatingTable");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ShopRatingTable", newName: "ShopRatingTables");
            RenameTable(name: "dbo.ItemsTable", newName: "ItemsTables");
            RenameTable(name: "dbo.CommentsTable", newName: "CommentsTables");
        }
    }
}
