namespace Price_comparison_engine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SukurtiNaudotojoDuomenis : DbMigration
    {
        public override void Up()
        {
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
        }

        public override void Down()
        {
            DropTable("dbo.NaudotojoDuomenys");
        }
    }
}
