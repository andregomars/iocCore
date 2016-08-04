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
    public class FunctionController : ApiController
    {
        private CoreDBModelsContext db = new CoreDBModelsContext();

        // GET: api/Function
        public IQueryable<Core_Function> Getcore_Function()
        {
            return db.core_Function;
        }

        // GET: api/Function/5
        [ResponseType(typeof(Core_Function))]
        public IHttpActionResult GetCore_Function(int id)
        {
            Core_Function core_Function = db.core_Function.Find(id);
            if (core_Function == null)
            {
                return NotFound();
            }

            return Ok(core_Function);
        }

        // PUT: api/Function/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCore_Function(int id, Core_Function core_Function)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != core_Function.ID)
            {
                return BadRequest();
            }

            db.Entry(core_Function).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Core_FunctionExists(id))
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

        // POST: api/Function
        [ResponseType(typeof(Core_Function))]
        public IHttpActionResult PostCore_Function(Core_Function core_Function)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.core_Function.Add(core_Function);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = core_Function.ID }, core_Function);
        }

        // POST: api/Function/Batch
        [ResponseType(typeof(Core_Function[]))]
        [Route("api/Function/Batch")]
        public IHttpActionResult PostCore_Functions([FromBody] Core_Function[] core_Functions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.core_Function.AddRange(core_Functions);
            db.SaveChanges();
            
            return Ok(db.core_Function);
        }

        // DELETE: api/Function/5
        [ResponseType(typeof(Core_Function))]
        public IHttpActionResult DeleteCore_Function(int id)
        {
            Core_Function core_Function = db.core_Function.Find(id);
            if (core_Function == null)
            {
                return NotFound();
            }

            db.core_Function.Remove(core_Function);
            db.SaveChanges();

            return Ok(core_Function);
        }

        // DELETE: api/Function/Batch
        [ResponseType(typeof(void))]
        [Route("api/Function/Batch")]
        public IHttpActionResult DeleteCore_Functions([FromBody] int[] ids)
        {
            Core_Function[] core_Functions = db.core_Function
                .Where<Core_Function>(r=>ids.Contains<int>( r.ID ))
                .ToArray<Core_Function>();

            if (core_Functions == null || core_Functions.Length == 0)
            {
                return NotFound();
            }

            db.core_Function.RemoveRange(core_Functions);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Core_FunctionExists(int id)
        {
            return db.core_Function.Count(e => e.ID == id) > 0;
        }
    }
}