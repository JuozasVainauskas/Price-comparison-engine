namespace Price_comparison_engine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SukurtiPuslapiuDuomenis : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PuslapiuDuomenys",
                c => new
                    {
                        PuslapioID = c.Int(nullable: false, identity: true),
                        PuslapioURL = c.String(),
                        ImgURL = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.PuslapioID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PuslapiuDuomenys");
        }
    }
}
