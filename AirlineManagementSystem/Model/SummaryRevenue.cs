using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportManagerSystem.Model
{
    class SummaryRevenue
    {
        public string Time { get; set; }
        public double Total { get; set; }
        public List<Ticket> Tickets { get; set; }

    }
}
