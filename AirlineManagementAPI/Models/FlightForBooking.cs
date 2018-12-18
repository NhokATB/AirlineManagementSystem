using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineManagementAPI.Models
{
    public class FlightForBooking
    {
        public FlightForBooking()
        {

        }

        public DateTime Outbound { get; set; }
        public TimeSpan Time { get; set; }
        public double Price { get; set; }
        public int NumberOfStop { get; set; }
        public string FlightNumbers { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Route { get; set; }
    }
}