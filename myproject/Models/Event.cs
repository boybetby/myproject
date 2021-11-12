using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myproject.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }
        public String EventName { get; set; }
        public String EventAddress { get; set; }
        public DateTime EventDate { get; set; }
        public String EventAbout { get; set; }
        public String EventImageBackground { get; set; }
        public int EventSeats { get; set; }

        public Event()
        {

        }
        public Event(int eventID, string eventName, string eventAddress, DateTime eventDate, string eventAbout, string eventImageBackground, int eventSeats)
        {
            EventID = eventID;
            EventName = eventName;
            EventAddress = eventAddress;
            EventDate = eventDate;
            EventAbout = eventAbout;
            EventImageBackground = eventImageBackground;
            EventSeats = eventSeats;
        }
    }
}