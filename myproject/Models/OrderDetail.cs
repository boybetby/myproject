using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myproject.Models
{
    public class OrderDetail
    {
        [Key]
        public int STT { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public int amount { get; set; }
    }
}