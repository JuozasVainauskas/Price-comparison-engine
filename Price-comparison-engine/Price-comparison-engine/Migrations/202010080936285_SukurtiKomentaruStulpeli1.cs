namespace Price_comparison_engine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SukurtiKomentaruStulpeli1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NaudotojoDuomenys", "Komentaras", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.NaudotojoDuomenys", "Komentaras");
        }
    }
}