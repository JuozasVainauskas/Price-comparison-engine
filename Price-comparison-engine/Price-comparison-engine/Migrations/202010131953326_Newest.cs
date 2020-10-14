namespace Price_comparison_engine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Newest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemsTable",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        PageUrl = c.String(),
                        ImgUrl = c.String(),
                        ShopName = c.String(),
                        ItemName = c.String(),
                        Price = c.String(),
                        Keyword = c.String(),
                    })
                .PrimaryKey(t => t.ItemId);
            
            CreateTable(
                "dbo.ShopRatingTable",
                c => new
                    {
                        ShopId = c.Int(nullable: false, identity: true),
                        ShopName = c.String(),
                        VotesNumber = c.Int(nullable: false),
                        VotersNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ShopId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ShopRatingTable");
            DropTable("dbo.ItemsTable");
        }
    }
}
