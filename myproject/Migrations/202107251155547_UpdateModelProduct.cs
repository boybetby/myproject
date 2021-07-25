namespace myproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModelProduct : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "productname", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "category", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "image", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "image", c => c.String());
            AlterColumn("dbo.Products", "category", c => c.Int());
            AlterColumn("dbo.Products", "productname", c => c.String());
        }
    }
}
