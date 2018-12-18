using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineManagementAPI.Models
{
    public class SeatReport
    {
        public string From { get; set; }
        public string To { get; set; }
        public TimeSpan Time { get; set; }
        public int EconomyTicket { get; set; }
        public int BusinessTicket { get; set; }
        public int FirstClassTicket { get; set; }
        public int EconomySeat { get; set; }
        public int BusinessSeat { get; set; }
        public int FirstClassSeat { get; set; }
    }
}