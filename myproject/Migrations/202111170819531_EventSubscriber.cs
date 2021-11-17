namespace myproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventSubscriber : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventSubscribers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubscriberEmail = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EventSubscribers");
        }
    }
}
