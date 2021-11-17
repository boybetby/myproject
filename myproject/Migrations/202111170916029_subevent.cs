namespace myproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subevent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventSubscribers", "Event_EventID", c => c.Int());
            CreateIndex("dbo.EventSubscribers", "Event_EventID");
            AddForeignKey("dbo.EventSubscribers", "Event_EventID", "dbo.Events", "EventID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventSubscribers", "Event_EventID", "dbo.Events");
            DropIndex("dbo.EventSubscribers", new[] { "Event_EventID" });
            DropColumn("dbo.EventSubscribers", "Event_EventID");
        }
    }
}
