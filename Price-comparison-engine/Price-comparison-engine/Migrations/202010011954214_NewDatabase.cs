namespace Price_comparison_engine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PageData",
                c => new
                    {
                        PageID = c.Int(nullable: false, identity: true),
                        PageURL = c.String(),
                        ImgURL = c.String(),
                    })
                .PrimaryKey(t => t.PageID);
            
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
            
            CreateTable(
                "dbo.UserData",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        PasswordHash = c.String(),
                        PasswordSalt = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
            DropTable("dbo.NaudotojoDuomenys");
            DropTable("dbo.ParduotuviuDuomenys");
            DropTable("dbo.PuslapiuDuomenys");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PuslapiuDuomenys",
                c => new
                    {
                        PuslapioID = c.Int(nullable: false, identity: true),
                        PuslapioURL = c.String(),
                        ImgURL = c.String(),
                    })
                .PrimaryKey(t => t.PuslapioID);
            
            CreateTable(
                "dbo.ParduotuviuDuomenys",
                c => new
                    {
                        ParduotuvesID = c.Int(nullable: false, identity: true),
                        ParduotuvesPavadinimas = c.String(),
                        BalsuSuma = c.Double(nullable: false),
                        BalsavusiuSkaicius = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ParduotuvesID);
            
            CreateTable(
                "dbo.NaudotojoDuomenys",
                c => new
                    {
                        NaudotojoID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        SlaptazodzioHash = c.String(),
                        SlaptazodzioSalt = c.String(),
                    })
                .PrimaryKey(t => t.NaudotojoID);
            
            DropTable("dbo.UserData");
            DropTable("dbo.ShopRatingData");
            DropTable("dbo.PageData");
        }
    }
}
