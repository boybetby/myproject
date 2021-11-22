namespace myproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guide : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guides",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GuideTitle = c.String(),
                        GuideDescription = c.String(),
                        Guide_Light = c.String(),
                        Guide_Water = c.String(),
                        Guide_Petfriendly = c.String(),
                        Guide_Level = c.String(),
                        Guide_Tip = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Guides");
        }
    }
}
