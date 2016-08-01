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
    public class UserRoleController : ApiController
    {
        private CoreDBModelsContext db = new CoreDBModelsContext();

        // GET: api/UserRole
        public IQueryable<Core_UserRole> Getcore_UserRole()
        {
            return db.core_UserRole;
        }

        // GET: api/UserRole/5
        [ResponseType(typeof(Core_UserRole))]
        public IHttpActionResult GetCore_UserRole(int id)
        {
            Core_UserRole core_UserRole = db.core_UserRole.Find(id);
            if (core_UserRole == null)
            {
                return NotFound();
            }

            return Ok(core_UserRole);
        }

        // PUT: api/UserRole/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCore_UserRole(int id, Core_UserRole core_UserRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != core_UserRole.ID)
            {
                return BadRequest();
            }

            db.Entry(core_UserRole).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Core_UserRoleExists(id))
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

        // POST: api/UserRole
        [ResponseType(typeof(Core_UserRole))]
        public IHttpActionResult PostCore_UserRole(Core_UserRole core_UserRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.core_UserRole.Add(core_UserRole);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = core_UserRole.ID }, core_UserRole);
        }

        // DELETE: api/UserRole/5
        [ResponseType(typeof(Core_UserRole))]
        public IHttpActionResult DeleteCore_UserRole(int id)
        {
            Core_UserRole core_UserRole = db.core_UserRole.Find(id);
            if (core_UserRole == null)
            {
                return NotFound();
            }

            db.core_UserRole.Remove(core_UserRole);
            db.SaveChanges();

            return Ok(core_UserRole);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Core_UserRoleExists(int id)
        {
            return db.core_UserRole.Count(e => e.ID == id) > 0;
        }
    }
}