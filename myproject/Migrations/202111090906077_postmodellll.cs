namespace myproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postmodellll : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        PostAuthor = c.String(),
                        PostDate = c.DateTime(nullable: false),
                        PostContent = c.String(),
                        PostLike = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostID);
            
            CreateTable(
                "dbo.PostComments",
                c => new
                    {
                        PostCommentID = c.Int(nullable: false, identity: true),
                        PostCommentAuthor = c.String(),
                        PostCommentContent = c.String(),
                        PostCommentDate = c.String(),
                        Post_PostID = c.Int(),
                    })
                .PrimaryKey(t => t.PostCommentID)
                .ForeignKey("dbo.Posts", t => t.Post_PostID)
                .Index(t => t.Post_PostID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostComments", "Post_PostID", "dbo.Posts");
            DropIndex("dbo.PostComments", new[] { "Post_PostID" });
            DropTable("dbo.PostComments");
            DropTable("dbo.Posts");
        }
    }
}
