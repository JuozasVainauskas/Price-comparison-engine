namespace Price_comparison_engine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SukurtiParduotuviuDuomenis : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParduotuviuDuomenys",
                c => new
                    {
                        ParduotuvesID = c.Int(nullable: false, identity: true),
                        ParduotuvesPavadinimas = c.String(),
                        BalsuSuma = c.Int(nullable: false),
                        BalsavusiuSkaicius = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ParduotuvesID);
        }
        
        public override void Down()
        {
            DropTable("dbo.ParduotuviuDuomenys");
        }
    }
}
