namespace myproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class address : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Province", c => c.String());
            AddColumn("dbo.Orders", "District", c => c.String());
            AddColumn("dbo.Orders", "Ward", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Ward");
            DropColumn("dbo.Orders", "District");
            DropColumn("dbo.Orders", "Province");
        }
    }
}
