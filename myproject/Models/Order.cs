using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myproject.Models
{
    public class Order
    {
        public string OrderID { get; set; }
        public int UserID { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone number is Required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",ErrorMessage = "Entered phone format is not valid.")]
        public string PhoneNumber { get; set; }
    
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        [Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }
        public long TotalPrice { get; set; }
        public DateTime Date { get; set; }

        public bool Checked { get; set; } = false;

        public string PaymentMethod { get; set; }
    }
}