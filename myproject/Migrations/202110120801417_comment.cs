namespace myproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        commentID = c.Int(nullable: false, identity: true),
                        comment = c.String(),
                        star = c.Int(nullable: false),
                        product_productID = c.Int(),
                    })
                .PrimaryKey(t => t.commentID)
                .ForeignKey("dbo.Products", t => t.product_productID)
                .Index(t => t.product_productID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "product_productID", "dbo.Products");
            DropIndex("dbo.Comments", new[] { "product_productID" });
            DropTable("dbo.Comments");
        }
    }
}
