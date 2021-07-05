using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myproject.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"^([a-zA-Z0-9_-.]+)@(([[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.)|(([a-zA-Z0-9-]+.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone number is Required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }
        public long TotalPrice { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}