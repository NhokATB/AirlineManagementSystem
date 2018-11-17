using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportManagerSystem.Model
{
    class NewTicket
    {
        public Ticket Ticket { get; set; }
        public string FullName { get; set; }
        public string Route { get; set; }
        public double Price { get; set; }
    }
}
