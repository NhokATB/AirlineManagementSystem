using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportManagerSystem.Model
{
    internal class RevenueOfRoute
    {
        public string IATACodeOfDeparture { get; set; }
        public string IATACodeOfArrival { get; set; }
        public double Revenue { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
