using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myproject.Models
{
    public class CartList
    {

        List<Cart> cartlist { get; set; }
        
        public void AddCart(Cart cart)
        {
            cartlist.Add(cart);
        }


        
    }
}