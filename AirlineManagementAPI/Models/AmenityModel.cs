using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineManagementAPI.Models
{
    public class AmenityModel
    {
        public int ID { get; set; }
        public string Service { get; set; }
        public decimal Price { get; set; }
    }
}