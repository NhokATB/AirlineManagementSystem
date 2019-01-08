using AirlineManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace AirlineManagementAPI.Controllers
{
    public class RevenueController : ApiController
    {
        private AirlineManagementSystemEntities db = new AirlineManagementSystemEntities();

        [HttpGet]
        [ResponseType(typeof(TicketRevenue))]
        public IHttpActionResult GetRevenueFromTickets(string date)
        {
            var d = DateTime.Parse(date);
            var tickets = db.Schedules.Where(t => t.Date == d).SelectMany(t => t.Tickets).ToList();

            List<TicketRevenue> ticketRevenues = new List<TicketRevenue>();

            var eTickets = tickets.Where(t => t.CabinTypeID == 1).ToList();
            var bTickets = tickets.Where(t => t.CabinTypeID == 2).ToList();
            var fTickets = tickets.Where(t => t.CabinTypeID == 3).ToList();

            ticketRevenues.Add(new TicketRevenue() { Revenue = GetPrice(eTickets, 1), CabinType = "Economy class" });
            ticketRevenues.Add(new TicketRevenue() { Revenue = GetPrice(eTickets, 2), CabinType = "Business class" });
            ticketRevenues.Add(new TicketRevenue() { Revenue = GetPrice(eTickets, 3), CabinType = "First class" });

            return Ok(ticketRevenues);
        }

        [HttpGet]
        [ResponseType(typeof(AmenityRevenue))]
        public IHttpActionResult GetRevenueFromAmenities(string date)
        {
            var amenities = db.Amenities.Where(t => t.Price != 0).ToList();
            var d = DateTime.Parse(date);
            var tickets = db.Schedules.Where(t => t.Date == d).SelectMany(t => t.Tickets).ToList();
            var amenTickets = tickets.SelectMany(t => t.AmenitiesTickets).ToList();

            List<AmenityRevenue> amenityRevenues = new List<AmenityRevenue>();

            foreach (var item in amenities)
            {
                amenityRevenues.Add(new AmenityRevenue() { Service = item.Service, Revenue = amenTickets.Where(t => t.AmenityID == item.ID).Sum(t => (int?)t.Price) ?? 0 });
            }

            return Ok(amenityRevenues);
        }

        [HttpGet]
        [ResponseType(typeof(OfficeRevenue))]
        public IHttpActionResult GetRevenueByOffice(string date)
        {
            var d = DateTime.Parse(date);

            var officeRevenues = db.Offices.Select(t => new OfficeRevenue
            {
                Title = t.Title,
                Tickets = t.Users.SelectMany(k => k.Tickets).Where(k => k.Schedule.Date == d).ToList(),
                Revenue = 0.0
            }).ToList();

            foreach (var item in officeRevenues)
            {
                item.Revenue += GetPrice(item.Tickets.Where(t => t.CabinTypeID == 1).ToList(), 1);
                item.Revenue += GetPrice(item.Tickets.Where(t => t.CabinTypeID == 2).ToList(), 2);
                item.Revenue += GetPrice(item.Tickets.Where(t => t.CabinTypeID == 3).ToList(), 3);
                item.Revenue += item.Tickets.Sum(t => t.AmenitiesTickets.Sum(k => (int?)k.Price)) ?? 0;

                item.Tickets = null;
            }

            return Ok(officeRevenues);
        }
        public static double GetPrice(List<Ticket> tickets, int cabinId)
        {
            double price = tickets.Sum(t => (int)t.Schedule.EconomyPrice);

            double bprice = Math.Floor(price * 1.35);
            double fprice = Math.Floor(bprice * 1.3);

            return cabinId == 1 ? price : (cabinId == 2 ? bprice : fprice);
        }
    }
}
