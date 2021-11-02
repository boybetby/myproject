namespace myproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class detail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Details",
                c => new
                    {
                        STT = c.Int(nullable: false, identity: true),
                        OrderID = c.String(),
                        ProductID = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.STT);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Details");
        }
    }
}
