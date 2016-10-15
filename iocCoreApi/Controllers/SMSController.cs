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
using System.Text.RegularExpressions;

namespace iocCoreApi.Controllers
{
    public class SMSController : ApiController
    {
        private CoreDBModelsContext db = new CoreDBModelsContext();

        // GET: api/SMS
        public IQueryable<Core_SMS> GetCore_SMS()
        {
            return db.Core_SMS;
        }

        // GET: api/SMS?status={status}
        public IQueryable<Core_SMS> GetCore_SMS(string status)
        {
            if (string.IsNullOrEmpty(status) || !Regex.Match(status, "^(0|1|2)$").Success)
            {
                return db.Core_SMS;
            }

            return db.Core_SMS.Where<Core_SMS>(r => r.Status == status);
        }

        // GET: api/SMS/5
        [ResponseType(typeof(Core_SMS))]
        public IHttpActionResult GetCore_SMS(int id)
        {
            Core_SMS core_SMS = db.Core_SMS.Find(id);
            if (core_SMS == null)
            {
                return NotFound();
            }

            return Ok(core_SMS);
        }
        
        // PUT: api/SMS/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCore_SMS(int id, Core_SMS core_SMS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != core_SMS.ID)
            {
                return BadRequest();
            }

            db.Entry(core_SMS).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Core_SMSExists(id))
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

        // POST: api/SMS
        [ResponseType(typeof(Core_SMS))]
        public IHttpActionResult PostCore_SMS(Core_SMS core_SMS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Core_SMS.Add(core_SMS);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = core_SMS.ID }, core_SMS);
        }

        // DELETE: api/SMS/5
        [ResponseType(typeof(Core_SMS))]
        public IHttpActionResult DeleteCore_SMS(int id)
        {
            Core_SMS core_SMS = db.Core_SMS.Find(id);
            if (core_SMS == null)
            {
                return NotFound();
            }

            db.Core_SMS.Remove(core_SMS);
            db.SaveChanges();

            return Ok(core_SMS);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Core_SMSExists(int id)
        {
            return db.Core_SMS.Count(e => e.ID == id) > 0;
        }
    }
}