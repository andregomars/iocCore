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
    public class PermissionController : ApiController
    {
        private CoreDBModelsContext db = new CoreDBModelsContext();

        // GET: api/Permission
        public IQueryable<Core_Permission> Getcore_Permission()
        {
            return db.core_Permission;
        }

        // GET: api/Permission/5
        [ResponseType(typeof(Core_Permission))]
        public IHttpActionResult GetCore_Permission(int id)
        {
            Core_Permission core_Permission = db.core_Permission.Find(id);
            if (core_Permission == null)
            {
                return NotFound();
            }

            return Ok(core_Permission);
        }

        // PUT: api/Permission/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCore_Permission(int id, Core_Permission core_Permission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != core_Permission.ID)
            {
                return BadRequest();
            }

            db.Entry(core_Permission).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Core_PermissionExists(id))
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

        // POST: api/Permission
        [ResponseType(typeof(Core_Permission))]
        public IHttpActionResult PostCore_Permission(Core_Permission core_Permission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.core_Permission.Add(core_Permission);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = core_Permission.ID }, core_Permission);
        }

        // POST: api/Permission/Batch
        [ResponseType(typeof(Core_Permission[]))]
        [Route("api/Permission/Batch")]
        public IHttpActionResult PostCore_Permissions([FromBody] Core_Permission[] core_Permissions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.core_Permission.AddRange(core_Permissions);
            db.SaveChanges();

            return Ok(db.core_Permission);
        }

        // DELETE: api/Permission/5
        [ResponseType(typeof(Core_Permission))]
        public IHttpActionResult DeleteCore_Permission(int id)
        {
            Core_Permission core_Permission = db.core_Permission.Find(id);
            if (core_Permission == null)
            {
                return NotFound();
            }

            db.core_Permission.Remove(core_Permission);
            db.SaveChanges();

            return Ok(core_Permission);
        }

        // DELETE: api/Permission/Batch
        [ResponseType(typeof(void))]
        [Route("api/Permission/Batch")]
        public IHttpActionResult DeleteCore_Permissions(int[] ids)
        {
            Core_Permission[] core_Permissions = db.core_Permission
                .Where<Core_Permission>(r => ids.Contains<int>(r.ID))
                .ToArray<Core_Permission>();

            if (core_Permissions == null || core_Permissions.Length == 0)
            {
                return NotFound();
            }

            db.core_Permission.RemoveRange(core_Permissions);
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

        private bool Core_PermissionExists(int id)
        {
            return db.core_Permission.Count(e => e.ID == id) > 0;
        }
    }
}