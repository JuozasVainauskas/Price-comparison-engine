namespace Price_comparison_engine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SukurtiPuslapiuDuomenis1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PuslapiuDuomenys", "ImgURL", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PuslapiuDuomenys", "ImgURL", c => c.Double(nullable: false));
        }
    }
}
