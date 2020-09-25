namespace Price_comparison_engine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SukurtiDuomenuStruktura : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DuomenuStrukturos",
                c => new
                    {
                        NaudotojoID = c.Int(nullable: false, identity: true),
                        NaudotojoEmail = c.String(),
                        NaudotojoSlaptazodis = c.String(),
                    })
                .PrimaryKey(t => t.NaudotojoID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DuomenuStrukturos");
        }
    }
}
