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
using AeroAPI.Models;

namespace AeroAPI.Controllers
{
    public class PassageirosController : ApiController
    {
        private AeroAPIEntities db = new AeroAPIEntities();

        // GET: api/Passageiros
        public IQueryable<Passageiros> GetPassageiros()
        {
            return db.Passageiros;
        }

        // GET: api/Passageiros/5
        [ResponseType(typeof(Passageiros))]
        public IHttpActionResult GetPassageiros(int id)
        {
            Passageiros passageiros = db.Passageiros.Find(id);
            if (passageiros == null)
            {
                return NotFound();
            }

            return Ok(passageiros);
        }

        // PUT: api/Passageiros/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPassageiros(int id, Passageiros passageiros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != passageiros.Id)
            {
                return BadRequest();
            }

            db.Entry(passageiros).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PassageirosExists(id))
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

        // POST: api/Passageiros
        [ResponseType(typeof(Passageiros))]
        public IHttpActionResult PostPassageiros(Passageiros passageiros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Passageiros.Add(passageiros);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = passageiros.Id }, passageiros);
        }

        // DELETE: api/Passageiros/5
        [ResponseType(typeof(Passageiros))]
        public IHttpActionResult DeletePassageiros(int id)
        {
            Passageiros passageiros = db.Passageiros.Find(id);
            if (passageiros == null)
            {
                return NotFound();
            }

            db.Passageiros.Remove(passageiros);
            db.SaveChanges();

            return Ok(passageiros);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PassageirosExists(int id)
        {
            return db.Passageiros.Count(e => e.Id == id) > 0;
        }
    }
}