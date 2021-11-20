namespace myproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Email", c => c.String());
            AddColumn("dbo.Users", "PhoneNumber", c => c.String());
            AddColumn("dbo.Users", "Province", c => c.String());
            AddColumn("dbo.Users", "District", c => c.String());
            AddColumn("dbo.Users", "Ward", c => c.String());
            AddColumn("dbo.Users", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Address");
            DropColumn("dbo.Users", "Ward");
            DropColumn("dbo.Users", "District");
            DropColumn("dbo.Users", "Province");
            DropColumn("dbo.Users", "PhoneNumber");
            DropColumn("dbo.Users", "Email");
        }
    }
}
