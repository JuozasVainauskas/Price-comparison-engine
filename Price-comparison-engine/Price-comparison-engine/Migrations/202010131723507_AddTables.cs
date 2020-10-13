namespace Price_comparison_engine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTables : DbMigration
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
            CreateTable(
                "dbo.SomethingNews",
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
            
            CreateTable(
                "dbo.PrekiuDuomenys",
                c => new
                    {
                        PrekiuID = c.Int(nullable: false, identity: true),
                        PuslapioURL = c.String(),
                        ImgURL = c.String(),
                        ParduotuvesVardas = c.String(),
                        PrekesVardas = c.String(),
                        PrekesKaina = c.String(),
                        RaktinisZodis = c.String(),
                    })
                .PrimaryKey(t => t.PrekiuID);
            
            CreateTable(
                "dbo.ParduotuviuDuomenys",
                c => new
                    {
                        ParduotuvesID = c.Int(nullable: false, identity: true),
                        ParduotuvesPavadinimas = c.String(),
                        BalsuSuma = c.Double(nullable: false),
                        BalsavusiuSkaicius = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ParduotuvesID);
            
            DropTable("dbo.ShopRatingTables");
            DropTable("dbo.ItemsTables");
        }
    }
}
