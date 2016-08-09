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
        public IQueryable<Core_Permission> GetCore_Permission()
        {
            return db.Core_Permission;
        }

        // GET: api/Permission/5
        [ResponseType(typeof(Core_Permission))]
        public IHttpActionResult GetCore_Permission(int id)
        {
            Core_Permission Core_Permission = db.Core_Permission.Find(id);
            if (Core_Permission == null)
            {
                return NotFound();
            }

            return Ok(Core_Permission);
        }

        // PUT: api/Permission/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCore_Permission(int id, Core_Permission Core_Permission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Core_Permission.ID)
            {
                return BadRequest();
            }

            db.Entry(Core_Permission).State = EntityState.Modified;

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
        public IHttpActionResult PostCore_Permission(Core_Permission Core_Permission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Core_Permission.Add(Core_Permission);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Core_Permission.ID }, Core_Permission);
        }


        // POST: api/Permission/Batch
        [ResponseType(typeof(Core_Permission[]))]
        [Route("api/Permission/Batch")]
        public IHttpActionResult PostCore_Permissions([FromBody] Core_Permission[] Core_Permissions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.Core_Permission");
            db.Core_Permission.AddRange(Core_Permissions);
            db.SaveChanges();

            return Ok(db.Core_Permission);
        }

        // DELETE: api/Permission/5
        [ResponseType(typeof(Core_Permission))]
        public IHttpActionResult DeleteCore_Permission(int id)
        {
            Core_Permission Core_Permission = db.Core_Permission.Find(id);
            if (Core_Permission == null)
            {
                return NotFound();
            }

            db.Core_Permission.Remove(Core_Permission);
            db.SaveChanges();

            return Ok(Core_Permission);
        }

        // DELETE: api/Permission/Batch
        [ResponseType(typeof(void))]
        [Route("api/Permission/Batch")]
        public IHttpActionResult DeleteCore_Permissions(int[] ids)
        {
            Core_Permission[] Core_Permissions = db.Core_Permission
                .Where<Core_Permission>(r => ids.Contains<int>(r.ID))
                .ToArray<Core_Permission>();

            if (Core_Permissions == null || Core_Permissions.Length == 0)
            {
                return NotFound();
            }

            db.Core_Permission.RemoveRange(Core_Permissions);
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
            return db.Core_Permission.Count(e => e.ID == id) > 0;
        }


        /*
        // POST: api/Permission/Batch
        [ResponseType(typeof(Core_Permission[]))]
        [Route("api/Permission/Batch")]
        public IHttpActionResult PostCore_Permissions([FromBody] Core_Permission[] Core_Permissions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Core_Permission.AddRange(Core_Permissions);
            db.SaveChanges();

            return Ok(db.Core_Permission);
        }
        */

    }
}