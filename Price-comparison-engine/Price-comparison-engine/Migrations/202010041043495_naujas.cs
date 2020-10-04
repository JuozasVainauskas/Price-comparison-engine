namespace Price_comparison_engine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class naujas : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NaudotojoDuomenys", "ArBalsavo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NaudotojoDuomenys", "ArBalsavo", c => c.Int(nullable: false));
        }
    }
}
