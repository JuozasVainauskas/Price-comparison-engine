namespace Price_comparison_engine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SukurtiParduotuviuDuomenis1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ParduotuviuDuomenys", "BalsuSuma", c => c.Double(nullable: false));
            AlterColumn("dbo.ParduotuviuDuomenys", "BalsavusiuSkaicius", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ParduotuviuDuomenys", "BalsavusiuSkaicius", c => c.Int(nullable: false));
            AlterColumn("dbo.ParduotuviuDuomenys", "BalsuSuma", c => c.Int(nullable: false));
        }
    }
}
