namespace Price_comparison_engine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Newww : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.NaudotojoDuomenys");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.NaudotojoDuomenys",
                c => new
                    {
                        NaudotojoID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        SlaptazodzioHash = c.String(),
                        SlaptazodzioSalt = c.String(),
                        ArBalsavo = c.String(),
                        Komentaras = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.NaudotojoID);
            
        }
    }
}
