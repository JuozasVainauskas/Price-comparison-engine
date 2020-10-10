namespace Price_comparison_engine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Naujas : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NaudotojoDuomenys", "Role", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NaudotojoDuomenys", "Role", c => c.Int(nullable: false));
        }
    }
}
