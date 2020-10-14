namespace Price_comparison_engine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Neww : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserData",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        PasswordHash = c.String(),
                        PasswordSalt = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserData");
        }
    }
}
