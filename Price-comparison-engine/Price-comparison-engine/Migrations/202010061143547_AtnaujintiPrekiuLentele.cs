namespace Price_comparison_engine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtnaujintiPrekiuLentele : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PrekiuDuomenys", "RaktinisZodis", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PrekiuDuomenys", "RaktinisZodis");
        }
    }
}
