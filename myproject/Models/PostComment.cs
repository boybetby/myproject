using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myproject.Models
{
    public class PostComment
    {
        public int PostCommentID { get; set; }
        public String PostCommentAuthor { get; set; }
        public String PostCommentContent { get; set; }
        public String PostCommentDate { get; set; }

        public PostComment()
        {

        }
        public PostComment(int postCommentID, string postCommentAuthor, string postCommentContent, string postCommentDate)
        {
            PostCommentID = postCommentID;
            PostCommentAuthor = postCommentAuthor;
            PostCommentContent = postCommentContent;
            PostCommentDate = postCommentDate;
        }
    }
}