namespace myproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hastag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "clade_hashtag", c => c.String());
            AddColumn("dbo.Products", "family_hasttag", c => c.String());
            AddColumn("dbo.Products", "difficulty_hashtag", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "difficulty_hashtag");
            DropColumn("dbo.Products", "family_hasttag");
            DropColumn("dbo.Products", "clade_hashtag");
        }
    }
}
