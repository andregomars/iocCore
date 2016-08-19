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
using iocCoreApi.Models;

namespace iocCoreApi.Controllers
{
    public class IO_CompanyController : ApiController
    {
        private IO_OnlineDBModelsContext db = new IO_OnlineDBModelsContext();

        // GET: api/IO_Company
        public IQueryable<IO_Company> GetIO_Company()
        {
            return db.IO_Company;
        }

        // GET: api/IO_Company/5
        [ResponseType(typeof(IO_Company))]
        public IHttpActionResult GetIO_Company(int id)
        {
            IO_Company iO_Company = db.IO_Company.Find(id);
            if (iO_Company == null)
            {
                return NotFound();
            }

            return Ok(iO_Company);
        }

        // PUT: api/IO_Company/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIO_Company(int id, IO_Company iO_Company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iO_Company.CompanyId)
            {
                return BadRequest();
            }

            db.Entry(iO_Company).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IO_CompanyExists(id))
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

        // POST: api/IO_Company
        [ResponseType(typeof(IO_Company))]
        public IHttpActionResult PostIO_Company(IO_Company iO_Company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IO_Company.Add(iO_Company);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = iO_Company.CompanyId }, iO_Company);
        }

        // DELETE: api/IO_Company/5
        [ResponseType(typeof(IO_Company))]
        public IHttpActionResult DeleteIO_Company(int id)
        {
            IO_Company iO_Company = db.IO_Company.Find(id);
            if (iO_Company == null)
            {
                return NotFound();
            }

            db.IO_Company.Remove(iO_Company);
            db.SaveChanges();

            return Ok(iO_Company);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IO_CompanyExists(int id)
        {
            return db.IO_Company.Count(e => e.CompanyId == id) > 0;
        }
    }
}