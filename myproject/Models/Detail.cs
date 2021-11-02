using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myproject.Models
{
    public class Detail
    {
        [Key]
        public int STT { get; set; }
        public String OrderID { get; set; }
        public int ProductID { get; set; }
        public int Amount { get; set; }

    }
}