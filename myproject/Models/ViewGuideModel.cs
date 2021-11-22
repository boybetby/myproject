using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myproject.Models
{
    public class ViewGuideModel
    {
        Guide guide { get; set; }
        List<Product> products { get; set; }
    }
}