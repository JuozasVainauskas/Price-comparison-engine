namespace Price_comparison_engine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SukurtiNaujaLentele : DbMigration
    {
        public override void Up()
        {
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
                    })
                .PrimaryKey(t => t.PrekiuID);
            
            AlterColumn("dbo.ParduotuviuDuomenys", "BalsavusiuSkaicius", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ParduotuviuDuomenys", "BalsavusiuSkaicius", c => c.Double(nullable: false));
            DropTable("dbo.PrekiuDuomenys");
        }
    }
}
