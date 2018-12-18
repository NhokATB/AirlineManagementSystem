using AirlineManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace AirlineManagementAPI.Controllers
{
    public class BusinessController : ApiController
    {
        private AirlineManagementSystemEntities db = new AirlineManagementSystemEntities();

        [HttpGet]
        [ResponseType(typeof(bool))]
        public IHttpActionResult Login(string email, string password)
        {
            User user = db.Users.FirstOrDefault(t => t.Email == email && t.Password == password);
            if (user == null)
            {
                return Ok(false + "" + user.Role.Title);
            }

            return Ok(true + "" + user.Role.Title);
        }

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

        [HttpGet]
        [ResponseType(typeof(AirportModel))]
        public IHttpActionResult GetAirports()
        {
            var airports = db.Airports.ToList();

            return Ok(airports.Select(t => new AirportModel
            {
                IATACode = t.IATACode,
                Name = t.Name
            }).ToList());
        }

        [HttpGet]
        [ResponseType(typeof(AmenityModel))]
        public IHttpActionResult GetAmenities()
        {
            var amenities = db.Amenities.ToList();

            return Ok(amenities.Select(t => new AmenityModel
            {
                ID = t.ID,
                Service = t.Service,
                Price = t.Price
            }).ToList());
        }

        [HttpGet]
        [ResponseType(typeof(TicketModel))]
        public IHttpActionResult GetTicketInformation(string passport)
        {
            var tickets = db.Tickets.Where(k => k.PassportNumber == passport).ToList().OrderByDescending(t => t.Schedule.Date + t.Schedule.Time).ToList();
            List<TicketModel> ticketModels = new List<TicketModel>();

            foreach (var item in tickets)
            {
                ticketModels.Add(new TicketModel(item));
            }

            return Ok(ticketModels);
        }

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

        [HttpGet]
        [ResponseType(typeof(FlightForBooking))]
        public IHttpActionResult SearchFlights(string from, string to, string date)
        {
            List<FlightForBooking> flights = new List<FlightForBooking>();
            flights = SearchFlight(from, to, true, DateTime.Parse(date));

            return Ok(flights);
        }

        public List<FlightForBooking> SearchFlight(string from, string to, bool? isChecked, DateTime date)
        {
            List<FlightForBooking> flights = new List<FlightForBooking>();

            var before = date.AddDays(-3);
            var after = date.AddDays(3);
            var sch1 = db.Schedules.Where(t => t.Date >= before && t.Date <= after && t.Route.Airport.IATACode == from && t.Route.Airport1.IATACode == to && t.Confirmed).ToList();
            if (!isChecked.Value)
            {
                sch1 = sch1.Where(t => t.Date == date).ToList();
            }

            foreach (var item in sch1)
            {
                flights.Add(new FlightForBooking()
                {
                    From = from,
                    To = to,
                    NumberOfStop = 0,
                    Outbound = item.Date,
                    Time = item.Time,
                    Price = (int)item.EconomyPrice,
                    Route = item.Route.Airport.IATACode + " - " + item.Route.Airport1.IATACode,
                    FlightNumbers = $"[{item.FlightNumber}]"
                });
            }

            flights.AddRange(IndirectFlight1(from, to, isChecked.Value, date));

            return flights;
        }

        private List<FlightForBooking> IndirectFlight1(string from, string to, bool isChecked, DateTime date)
        {
            List<FlightForBooking> results = new List<FlightForBooking>();
            var before = date.AddDays(-3);
            var after = date.AddDays(3);
            var sch1 = db.Schedules.Where(t => t.Date >= before && t.Date <= after && t.Route.Airport.IATACode == from && t.Confirmed).ToList();
            if (!isChecked)
            {
                sch1 = sch1.Where(t => t.Date == date).ToList();
            }

            foreach (var s1 in sch1)
            {
                var arrivalTime = (s1.Date + s1.Time).AddMinutes(s1.Route.FlightTime);
                var sch2 = db.Schedules.Where(t => t.Route.Airport.IATACode == s1.Route.Airport1.IATACode && t.Route.Airport1.IATACode == to && t.Confirmed).ToList();

                sch2 = sch2.Where(t => t.Date + t.Time >= arrivalTime).ToList();

                foreach (var s2 in sch2)
                {
                    results.Add(new FlightForBooking()
                    {
                        From = from,
                        To = to,
                        Outbound = s1.Date,
                        Time = s1.Time,
                        Price = (int)s1.EconomyPrice + (int)s2.EconomyPrice,
                        Route = s1.Route.Airport.IATACode + " - " + s2.Route.Airport.IATACode + " - " + s2.Route.Airport1.IATACode,
                        NumberOfStop = 1,
                        FlightNumbers = $"[{s1.FlightNumber}] - [{s2.FlightNumber}]",
                    });
                }
            }

            return results;
        }
        private List<FlightForBooking> IndirectFlight2(string from, string to, bool isChecked, DateTime date)
        {
            List<FlightForBooking> results = new List<FlightForBooking>();
            var before = date.AddDays(-3);
            var after = date.AddDays(3);

            var sch1 = db.Schedules.Where(t => t.Date >= before && t.Date <= after && t.Route.Airport.IATACode == from && t.Route.Airport1.IATACode != from && t.Route.Airport1.IATACode != to && t.Confirmed).ToList();
            if (!isChecked)
            {
                sch1 = sch1.Where(t => t.Date == date).ToList();
            }

            foreach (var s1 in sch1)
            {
                var arrivalTime = (s1.Date + s1.Time).AddMinutes(s1.Route.FlightTime);
                var sch2 = db.Schedules.Where(t => t.Route.Airport.IATACode == s1.Route.Airport1.IATACode && t.Route.Airport1.IATACode != from && t.Route.Airport1.IATACode != to && t.Confirmed).ToList();

                sch2 = sch2.Where(t => t.Date + t.Time >= arrivalTime).ToList();

                foreach (var s2 in sch2)
                {
                    arrivalTime = (s2.Date + s2.Time).AddMinutes(s2.Route.FlightTime);
                    var sch3 = db.Schedules.Where(t => t.Route.Airport.IATACode == s2.Route.Airport1.IATACode && t.Route.Airport1.IATACode == to && t.Confirmed).ToList();

                    sch3 = sch3.Where(t => t.Date + t.Time >= arrivalTime).ToList();

                    foreach (var s3 in sch3)
                    {
                        results.Add(new FlightForBooking()
                        {
                            From = from,
                            To = to,
                            Outbound = s1.Date,
                            Time = s1.Time,
                            Price = (int)s1.EconomyPrice + (int)s2.EconomyPrice + (int)s3.EconomyPrice,
                            Route = s1.Route.Airport.IATACode + " - " + s2.Route.Airport.IATACode + " - " + s3.Route.Airport.IATACode + " - " + s3.Route.Airport1.IATACode,
                            NumberOfStop = 2,
                            FlightNumbers = $"[{s1.FlightNumber}] - [{s2.FlightNumber}] - [{ s3.FlightNumber }]",
                        });
                    }
                }
            }

            return results;
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
