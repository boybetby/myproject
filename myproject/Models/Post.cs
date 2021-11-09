using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myproject.Models
{
    public class Post
    {
        public int PostID { get; set; }
        public String PostAuthor { get; set; }
        public DateTime PostDate { get; set; }
        public String PostContent { get; set; }
        public String PostImage { get; set; }
        public int PostLike { get; set; }
        public virtual ICollection<PostComment> PostComment { get; set; }

        public Post()
        {

        }

        public Post(int postID, string postAuthor, DateTime postDate, string postContent, string postImage, int postLike, ICollection<PostComment> postComment)
        {
            PostID = postID;
            PostAuthor = postAuthor;
            PostDate = postDate;
            PostContent = postContent;
            PostImage = postImage;
            PostLike = postLike;
            PostComment = postComment;
        }
    }
}