using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AirlineManagementAPI.Models;

namespace AirlineManagementAPI.Controllers
{
    public class SchedulesController : ApiController
    {
        private AirlineManagementSystemEntities db = new AirlineManagementSystemEntities();

        // GET: api/Schedules
        public IQueryable<Schedule> GetSchedules()
        {
            return db.Schedules;
        }

        // GET: api/Schedules/5
        [ResponseType(typeof(Schedule))]
        public IHttpActionResult GetSchedule(int id)
        {
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return NotFound();
            }

            return Ok(schedule);
        }

        // PUT: api/Schedules/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSchedule(int id, Schedule schedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != schedule.ID)
            {
                return BadRequest();
            }

            db.Entry(schedule).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Schedules
        [ResponseType(typeof(Schedule))]
        public IHttpActionResult PostSchedule(Schedule schedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Schedules.Add(schedule);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = schedule.ID }, schedule);
        }

        // DELETE: api/Schedules/5
        [ResponseType(typeof(Schedule))]
        public IHttpActionResult DeleteSchedule(int id)
        {
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return NotFound();
            }

            db.Schedules.Remove(schedule);
            db.SaveChanges();

            return Ok(schedule);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ScheduleExists(int id)
        {
            return db.Schedules.Count(e => e.ID == id) > 0;
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
            before = before < DateTime.Now.Date ? DateTime.Now.Date : before;

            var after = date.AddDays(3);
            var sch1 = db.Schedules.Where(t => t.Date >= before && t.Date <= after && t.Route.Airport.IATACode == from && t.Route.Airport1.IATACode == to && t.Confirmed).ToList();
            if (!isChecked.Value)
                sch1 = sch1.Where(t => t.Date == date).ToList();

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
            before = before < DateTime.Now.Date ? DateTime.Now.Date : before;

            var after = date.AddDays(3);
            var sch1 = db.Schedules.Where(t => t.Date >= before && t.Date <= after && t.Route.Airport.IATACode == from && t.Confirmed).ToList();
            if (!isChecked)
                sch1 = sch1.Where(t => t.Date == date).ToList();

            foreach (var s1 in sch1)
            {
                var arrivalTime = (s1.Date + s1.Time).AddMinutes(s1.Route.FlightTime + 60);
                var sch2 = db.Schedules.Where(t => t.Route.Airport.IATACode == s1.Route.Airport1.IATACode && t.Route.Airport1.IATACode == to && t.Confirmed).ToList();

                var limitTime = arrivalTime.AddHours(24);
                sch2 = sch2.Where(t => t.Date + t.Time >= arrivalTime && t.Date + t.Time <= limitTime).ToList();

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
            before = before < DateTime.Now.Date ? DateTime.Now.Date : before;

            var after = date.AddDays(3);
            var sch1 = db.Schedules.Where(t => t.Date >= before && t.Date <= after && t.Route.Airport.IATACode == from && t.Route.Airport1.IATACode != from && t.Route.Airport1.IATACode != to && t.Confirmed).ToList();
            if (!isChecked)
                sch1 = sch1.Where(t => t.Date == date).ToList();

            foreach (var s1 in sch1)
            {
                var arrivalTime = (s1.Date + s1.Time).AddMinutes(s1.Route.FlightTime + 60);
                var sch2 = db.Schedules.Where(t => t.Route.Airport.IATACode == s1.Route.Airport1.IATACode && t.Route.Airport1.IATACode != from && t.Route.Airport1.IATACode != to && t.Confirmed).ToList();

                var limitTime = arrivalTime.AddHours(24);
                sch2 = sch2.Where(t => t.Date + t.Time >= arrivalTime && t.Date + t.Time <= limitTime).ToList();

                foreach (var s2 in sch2)
                {
                    arrivalTime = (s2.Date + s2.Time).AddMinutes(s2.Route.FlightTime + 60);
                    var sch3 = db.Schedules.Where(t => t.Route.Airport.IATACode == s2.Route.Airport1.IATACode && t.Route.Airport1.IATACode == to && t.Confirmed).ToList();

                    limitTime = arrivalTime.AddHours(24);
                    sch3 = sch3.Where(t => t.Date + t.Time >= arrivalTime && t.Date + t.Time <= limitTime).ToList();

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
    }
}