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
    public class TicketsController : ApiController
    {
        private AirlineManagementSystemEntities db = new AirlineManagementSystemEntities();

        // GET: api/Tickets
        public IQueryable<Ticket> GetTickets()
        {
            return db.Tickets;
        }

        // GET: api/Tickets/5
        [ResponseType(typeof(Ticket))]
        public IHttpActionResult GetTicket(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return NotFound();
            }

            TicketModel ticketModel = new TicketModel(ticket);

            return Ok(ticketModel);
        }

        // PUT: api/Tickets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTicket(int id, Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ticket.ID)
            {
                return BadRequest();
            }

            db.Entry(ticket).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
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

        // POST: api/Tickets
        [ResponseType(typeof(Ticket))]
        public IHttpActionResult PostTicket(Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tickets.Add(ticket);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ticket.ID }, ticket);
        }

        // DELETE: api/Tickets/5
        [ResponseType(typeof(Ticket))]
        public IHttpActionResult DeleteTicket(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return NotFound();
            }

            db.Tickets.Remove(ticket);
            db.SaveChanges();

            return Ok(ticket);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TicketExists(int id)
        {
            return db.Tickets.Count(e => e.ID == id) > 0;
        }
    }
}