using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace myproject.Models
{
    public class Comment
    {
        public int commentID { get; set; }
        public Product product { get; set; }
        public String comment { get; set; }
        public int star { get; set; }

        public Comment()
        {

        }

        public Comment(Product product, string comment, int star)
        {
            this.product = product;
            this.comment = comment;
            this.star = star;
        }
    }
}