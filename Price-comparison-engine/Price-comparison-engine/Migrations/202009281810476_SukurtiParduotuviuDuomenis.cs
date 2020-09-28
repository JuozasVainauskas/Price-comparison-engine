namespace Price_comparison_engine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SukurtiParduotuviuDuomenis : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AvitelaDuomenys",
                c => new
                {
                    AvitelosID = c.Int(nullable: false, identity: true),
                    AvitelosBalsuSuma = c.Int(nullable: false),
                    AvitelosBalsavusiuSkaicius = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.AvitelosID);

            CreateTable(
                "dbo.ElektromarktDuomenys",
                c => new
                {
                    ElektromarktID = c.Int(nullable: false, identity: true),
                    ElektromarktBalsuSuma = c.Int(nullable: false),
                    ElektromarktBalsavusiuSkaicius = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ElektromarktID);

        }

        public override void Down()
        {
            DropTable("dbo.ElektromarktDuomenys");
            DropTable("dbo.AvitelaDuomenys");
        }
    }
}
