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
    public class CompanyController : ApiController
    {
        private CoreDBModelsContext db = new CoreDBModelsContext();

        // GET: api/Company
        public IQueryable<Core_Company> GetCore_Company()
        {
            return db.Core_Company;
        }

        // GET: api/Company/5
        [ResponseType(typeof(Core_Company))]
        public IHttpActionResult GetCore_Company(int id)
        {
            Core_Company core_Company = db.Core_Company.Find(id);
            if (core_Company == null)
            {
                return NotFound();
            }

            return Ok(core_Company);
        }

        // PUT: api/Company/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCore_Company(int id, Core_Company core_Company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != core_Company.ID)
            {
                return BadRequest();
            }

            db.Entry(core_Company).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Core_CompanyExists(id))
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

        // POST: api/Company
        [ResponseType(typeof(Core_Company))]
        public IHttpActionResult PostCore_Company(Core_Company core_Company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Core_Company.Add(core_Company);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = core_Company.ID }, core_Company);
        }

        // DELETE: api/Company/5
        [ResponseType(typeof(Core_Company))]
        public IHttpActionResult DeleteCore_Company(int id)
        {
            Core_Company core_Company = db.Core_Company.Find(id);
            if (core_Company == null)
            {
                return NotFound();
            }

            db.Core_Company.Remove(core_Company);
            db.SaveChanges();

            return Ok(core_Company);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Core_CompanyExists(int id)
        {
            return db.Core_Company.Count(e => e.ID == id) > 0;
        }
    }
}