using AirlineManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace AirlineManagementAPI.Controllers
{
    public class ReportController : ApiController
    {
        private AirlineManagementSystemEntities db = new AirlineManagementSystemEntities();

        [HttpGet]
        [ResponseType(typeof(SeatReport))]
        public IHttpActionResult SeatReport(string date)
        {
            var d = DateTime.Parse(date);
            var flights = db.Schedules.Where(t => t.Date == d).ToList();

            var seatReports = new List<SeatReport>();
            foreach (var item in flights)
            {
                seatReports.Add(new Models.SeatReport()
                {
                    From = item.Route.Airport.Name,
                    To = item.Route.Airport1.Name,
                    Time = item.Time,
                    BusinessSeat = item.Aircraft.BusinessSeats,
                    EconomySeat = item.Aircraft.EconomySeats,
                    FirstClassSeat = item.Aircraft.TotalSeats - item.Aircraft.EconomySeats - item.Aircraft.BusinessSeats,
                    EconomyTicket = item.Tickets.Count(t => t.CabinTypeID == 1),
                    BusinessTicket = item.Tickets.Count(t => t.CabinTypeID == 2),
                    FirstClassTicket = item.Tickets.Count(t => t.CabinTypeID == 3),
                });
            }

            return Ok(seatReports);
        }

    }
}
