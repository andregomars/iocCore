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
    public class RoleController : ApiController
    {
        private CoreDBModelsContext db = new CoreDBModelsContext();

        // GET: api/Role
        public IQueryable<Core_Role> Getcore_role()
        {
            return db.core_role;
        }

        // GET: api/Role/5
        [ResponseType(typeof(Core_Role))]
        public IHttpActionResult GetCore_Role(int id)
        {
            Core_Role core_Role = db.core_role.Find(id);
            if (core_Role == null)
            {
                return NotFound();
            }

            return Ok(core_Role);
        }

        // PUT: api/Role/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCore_Role(int id, Core_Role core_Role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != core_Role.ID)
            {
                return BadRequest();
            }

            db.Entry(core_Role).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Core_RoleExists(id))
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

        // POST: api/Role
        [ResponseType(typeof(Core_Role))]
        public IHttpActionResult PostCore_Role(Core_Role core_Role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.core_role.Add(core_Role);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = core_Role.ID }, core_Role);
        }

        // POST: api/Role/Batch
        [ResponseType(typeof(List<Core_Role>))]
        [Route("api/Role/Batch")]
        public IHttpActionResult PostCore_Roles(List<Core_Role> core_Roles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            db.core_role.AddRange(core_Roles);
            db.SaveChanges();

            return Ok(db.core_role);
        }

        // DELETE: api/Role/5
        [ResponseType(typeof(Core_Role))]
        public IHttpActionResult DeleteCore_Role(int id)
        {
            Core_Role core_Role = db.core_role.Find(id);
            if (core_Role == null)
            {
                return NotFound();
            }

            db.core_role.Remove(core_Role);
            db.SaveChanges();

            return Ok(core_Role);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Core_RoleExists(int id)
        {
            return db.core_role.Count(e => e.ID == id) > 0;
        }
    }
}