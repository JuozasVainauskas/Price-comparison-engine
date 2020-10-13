namespace Price_comparison_engine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.SavedItems");
            CreateTable(
                "dbo.CommentsTable",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        ShopId = c.Int(nullable: false),
                        Date = c.String(),
                        ServiceRating = c.Int(nullable: false),
                        ProductsQualityRating = c.Int(nullable: false),
                        DeliveryRating = c.Int(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.CommentId);
            CreateTable(
                    "dbo.SavedItems",
                    c => new
                    {
                        SavedItemId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        PageUrl = c.String(),
                        ImgUrl = c.String(),
                        ShopName = c.String(),
                        ItemName = c.String(),
                        Price = c.String(),
                    })
                .PrimaryKey(t => t.SavedItemId);

        }
        
        public override void Down()
        {
            DropTable("dbo.CommentsTable");
        }
    }
}
