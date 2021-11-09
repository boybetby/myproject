namespace myproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postmodel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "PostImage", c => c.String());
            DropColumn("dbo.Posts", "PostImages");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "PostImages", c => c.String());
            DropColumn("dbo.Posts", "PostImage");
        }
    }
}
