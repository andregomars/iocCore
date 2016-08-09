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
        public IQueryable<Core_Role> GetCore_role()
        {
            return db.Core_Role;
        }

        // GET: api/Role/5
        [ResponseType(typeof(Core_Role))]
        public IHttpActionResult GetCore_Role(int id)
        {
            Core_Role Core_Role = db.Core_Role.Find(id);
            if (Core_Role == null)
            {
                return NotFound();
            }

            return Ok(Core_Role);
        }

        // PUT: api/Role/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCore_Role(int id, Core_Role Core_Role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Core_Role.ID)
            {
                return BadRequest();
            }

            db.Entry(Core_Role).State = EntityState.Modified;

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
        public IHttpActionResult PostCore_Role(Core_Role Core_Role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Core_Role.Add(Core_Role);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Core_Role.ID }, Core_Role);
        }

        // POST: api/Role/Batch
        [ResponseType(typeof(List<Core_Role>))]
        [Route("api/Role/Batch")]
        public IHttpActionResult PostCore_Roles(List<Core_Role> Core_Roles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Core_Role> masterRoles = db.Core_Role.ToList<Core_Role>();

            //find out roles to update, and mark them in db model to modified
            List<Core_Role> rolesToUpdate =
                masterRoles.Where<Core_Role>(m => Core_Roles.Any<Core_Role>(r => m.RoleName.Equals(r.RoleName)))
                .ToList<Core_Role>();
            rolesToUpdate.ForEach(m =>
                { Core_Role row = Core_Roles.Find(r => m.RoleName.Equals(r.RoleName));
                    m.RoleType = row.RoleType;
                    m.RoleDescription = row.RoleDescription;
                    m.EditDate = DateTime.Now;
                    m.EditUser = row.EditUser;
                });
            rolesToUpdate.ForEach(m => db.Entry(m).State = EntityState.Modified );

            //find out roles to insert, and insert them into db model
            List<Core_Role> rolesToInsert =
                Core_Roles.Where<Core_Role>(r => !masterRoles.Any<Core_Role>(m => m.RoleName.Equals(r.RoleName)))
                .ToList<Core_Role>();
            db.Core_Role.AddRange(rolesToInsert);

            //find out roles to delete, and remove them from db model
            List<Core_Role> rolesToDelete =
                masterRoles.Where<Core_Role>(m => !Core_Roles.Any<Core_Role>(r => m.RoleName.Equals(r.RoleName)))
                .ToList<Core_Role>();
            db.Core_Role.RemoveRange(rolesToDelete);

            //commit db model changes and do the action in db
            db.SaveChanges();

            return Ok(db.Core_Role);
        }

        // DELETE: api/Role/5
        [ResponseType(typeof(Core_Role))]
        public IHttpActionResult DeleteCore_Role(int id)
        {
            Core_Role Core_Role = db.Core_Role.Find(id);
            if (Core_Role == null)
            {
                return NotFound();
            }

            db.Core_Role.Remove(Core_Role);
            db.SaveChanges();

            return Ok(Core_Role);
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
            return db.Core_Role.Count(e => e.ID == id) > 0;
        }
    }
}