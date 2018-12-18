using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineManagementAPI.Models
{
    public class OfficeRevenue
    {
        public string Title { get; set; }
        public List<Ticket> Tickets { get; set; }
        public double Revenue { get; set; }
    }
}