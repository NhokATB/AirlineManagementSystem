using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportManagerSystem.Model
{
    class Flight
    {
        public Flight()
        {
        }

        public List<Schedule> Flights { get; set; }
        public double Price { get; set; }
        public int NumberOfStop { get; set; }
        public string FlightNumbers { get; set; }
        public Schedule FirstFlight { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}
