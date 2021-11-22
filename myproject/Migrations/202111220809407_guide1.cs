namespace myproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guide1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guides", "clade_hashtag", c => c.String());
            AddColumn("dbo.Guides", "family_hasttag", c => c.String());
            AddColumn("dbo.Guides", "difficulty_hashtag", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guides", "difficulty_hashtag");
            DropColumn("dbo.Guides", "family_hasttag");
            DropColumn("dbo.Guides", "clade_hashtag");
        }
    }
}
