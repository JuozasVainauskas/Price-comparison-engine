namespace Price_comparison_engine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NaudotojoDuomenuUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NaudotojoDuomenys", "ArBalsavo", c => c.Int(nullable: false));
            AddColumn("dbo.NaudotojoDuomenys", "Role", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NaudotojoDuomenys", "Role");
            DropColumn("dbo.NaudotojoDuomenys", "ArBalsavo");
        }
    }
}
