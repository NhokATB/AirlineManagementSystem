using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AmonicAirlineApp
{
    class SeatReport
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