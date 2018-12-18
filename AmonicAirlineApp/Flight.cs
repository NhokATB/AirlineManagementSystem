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
    class Flight
    {
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