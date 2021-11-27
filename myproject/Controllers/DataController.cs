using myproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace myproject.Controllers
{
    public class DataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public double[] GetOrderChart()
        {
            double[] data = new double[6];
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;
            int y = 0;

            for (int i=5; i>=0; i--)
            {
                double earningmonthly = 0;
                var data_monthly = db.Orders.Where(m => m.Date.Month == (currentMonth - i) && m.Date.Year == currentYear).ToList();
                foreach (Order order in data_monthly)
                {
                    earningmonthly += order.TotalPrice;
                }
                data[y] = earningmonthly;
                y++;
            }

            return data;
        }
    }
}
