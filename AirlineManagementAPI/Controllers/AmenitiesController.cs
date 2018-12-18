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
    public class AmenitiesController : ApiController
    {
        private AirlineManagementSystemEntities db = new AirlineManagementSystemEntities();

        public List<AmenityModel> GetAllAmenities()
        {
            var amenities = db.Amenities.ToList();

            return amenities.Select(t => new AmenityModel
            {
                ID = t.ID,
                Service = t.Service,
                Price = t.Price
            }).ToList();
        }
        // GET: api/Amenities
        public IQueryable<Amenity> GetAmenities()
        {
            return db.Amenities;
        }

        // GET: api/Amenities/5
        [ResponseType(typeof(Amenity))]
        public IHttpActionResult GetAmenity(int id)
        {
            Amenity amenity = db.Amenities.Find(id);
            if (amenity == null)
            {
                return NotFound();
            }

            return Ok(amenity);
        }

        // PUT: api/Amenities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAmenity(int id, Amenity amenity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != amenity.ID)
            {
                return BadRequest();
            }

            db.Entry(amenity).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmenityExists(id))
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

        // POST: api/Amenities
        [ResponseType(typeof(Amenity))]
        public IHttpActionResult PostAmenity(Amenity amenity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Amenities.Add(amenity);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = amenity.ID }, amenity);
        }

        // DELETE: api/Amenities/5
        [ResponseType(typeof(Amenity))]
        public IHttpActionResult DeleteAmenity(int id)
        {
            Amenity amenity = db.Amenities.Find(id);
            if (amenity == null)
            {
                return NotFound();
            }

            db.Amenities.Remove(amenity);
            db.SaveChanges();

            return Ok(amenity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AmenityExists(int id)
        {
            return db.Amenities.Count(e => e.ID == id) > 0;
        }
    }
}