using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myproject.Models
{
    public class EventSubscriber
    {
        public int Id { get; set; }
        public string SubscriberEmail { get; set; }

        public EventSubscriber()
        {
        }

        public EventSubscriber(int id, string subscriberEmail)
        {
            Id = id;
            SubscriberEmail = subscriberEmail;
        }
    }
}