using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineManagementAPI.Models
{
    public class ScheduleModel
    {
        public System.DateTime Date { get; set; }
        public System.TimeSpan Time { get; set; }
        public string Aircraft { get; set; }
        public string Route { get; set; }
        public decimal EconomyPrice { get; set; }
        public bool Confirmed { get; set; }
        public string FlightNumber { get; set; }
        public Nullable<int> CrewId { get; set; }
        public string Gate { get; set; }

        public ScheduleModel()
        {

        }

        public ScheduleModel(Schedule schedule)
        {
            Aircraft = schedule.Aircraft.Name;
            Date = schedule.Date;
            Time = schedule.Time;
            Route = schedule.Route.Airport.IATACode + " - " + schedule.Route.Airport1.IATACode;
            EconomyPrice = schedule.EconomyPrice;
            Confirmed = schedule.Confirmed;
            FlightNumber = schedule.FlightNumber;
            CrewId = schedule.CrewId;
            Gate = schedule.Gate == null ? "None" : schedule.Gate.ToString();
        }
       
    }
}