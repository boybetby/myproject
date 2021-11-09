namespace myproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "PostImages", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "PostImages");
        }
    }
}
