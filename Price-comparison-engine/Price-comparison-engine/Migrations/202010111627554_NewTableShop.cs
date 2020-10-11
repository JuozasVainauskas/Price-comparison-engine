namespace Price_comparison_engine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTableShop : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SavedItems",
                c => new
                    {
                        SavedItemID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        PageURL = c.String(),
                        ImgURL = c.String(),
                        ShopName = c.String(),
                        ItemName = c.String(),
                        Price = c.String(),
                    })
                .PrimaryKey(t => t.SavedItemID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SavedItems");
        }
    }
}
