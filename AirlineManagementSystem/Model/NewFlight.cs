using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportManagerSystem.Model
{
    class NewFlight
    {
        //public DateTime Date { get; set; }
        //public TimeSpan Time { get; set; }
        //public Airport From { get; set; }
        //public Airport To { get; set; }
        //public string FlightNumber { get; set; }
        //public Aircraft Aircraft { get; set; }
        public double EconomyPrice { get; set; }
        public double BusinessPrice { get; set; }
        public double FirstClassPrice { get; set; }
        //public bool Confirmed { get; set; }
        public Schedule Schedule { get; set; }
    }
}
