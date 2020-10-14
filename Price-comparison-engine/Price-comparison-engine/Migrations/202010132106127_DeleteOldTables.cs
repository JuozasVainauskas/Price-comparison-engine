namespace Price_comparison_engine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteOldTables : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.PrekiuDuomenys");
        }
        
        public override void Down()
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
                        RaktinisZodis = c.String(),
                    })
                .PrimaryKey(t => t.PrekiuID);
            
        }
    }
}
