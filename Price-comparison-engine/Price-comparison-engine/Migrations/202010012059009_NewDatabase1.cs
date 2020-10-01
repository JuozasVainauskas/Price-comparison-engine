namespace Price_comparison_engine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewDatabase1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShopRatingData",
                c => new
                {
                    ShopID = c.Int(nullable: false, identity: true),
                    ShopName = c.String(),
                    VotesCount = c.Double(nullable: false),
                    VotersNumber = c.Double(nullable: false),
                })
                .PrimaryKey(t => t.ShopID);
        }
        
        public override void Down()
        {
        }
    }
}
