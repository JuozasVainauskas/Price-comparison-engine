namespace Price_comparison_engine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Newer : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.ParduotuviuDuomenys");
        }
        
        public override void Down()
        {
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
            
        }
    }
}
