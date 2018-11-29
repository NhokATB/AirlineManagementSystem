using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportManagerSystem.Model
{
    class NewFlight
    {
        public double EconomyPrice { get; set; }
        public double BusinessPrice { get; set; }
        public double FirstClassPrice { get; set; }
        public Schedule Schedule { get; set; }
        public string Aircraft { get; set; }
        public string Crew { get; set; }
    }
}
