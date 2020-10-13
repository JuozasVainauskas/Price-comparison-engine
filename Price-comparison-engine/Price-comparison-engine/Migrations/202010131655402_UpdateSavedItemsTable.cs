namespace Price_comparison_engine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSavedItemsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SavedItems",
                c => new
                    {
                        SavedItemId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        PageUrl = c.String(),
                        ImgUrl = c.String(),
                        ShopName = c.String(),
                        ItemName = c.String(),
                        Price = c.String(),
                    })
                .PrimaryKey(t => t.SavedItemId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SavedItems");
        }
    }
}
