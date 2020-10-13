namespace Price_comparison_engine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateCommentsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommentsTables",
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CommentsTables");
        }
    }
}
