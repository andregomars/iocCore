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

            List<Core_Role> masterRoles = db.core_role.ToList<Core_Role>();

            //find out roles to update, and mark them in db model to modified
            List<Core_Role> rolesToUpdate =
                masterRoles.Where<Core_Role>(m => core_Roles.Any<Core_Role>(r => m.RoleName.Equals(r.RoleName)))
                .ToList<Core_Role>();
            rolesToUpdate.ForEach(m =>
                { Core_Role row = core_Roles.Find(r => m.RoleName.Equals(r.RoleName));
                    m.RoleType = row.RoleType;
                    m.RoleDescription = row.RoleDescription;
                    m.EditDate = DateTime.Now;
                    m.EditUser = row.EditUser;
                    m.Status = row.Status;
                });
            rolesToUpdate.ForEach(m => db.Entry(m).State = EntityState.Modified );

            //find out roles to insert, and insert them into db model
            List<Core_Role> rolesToInsert =
                core_Roles.Where<Core_Role>(r => !masterRoles.Any<Core_Role>(m => m.RoleName.Equals(r.RoleName)))
                .ToList<Core_Role>();
            db.core_role.AddRange(rolesToInsert);

            //find out roles to delete, and remove them from db model
            List<Core_Role> rolesToDelete =
                masterRoles.Where<Core_Role>(m => !core_Roles.Any<Core_Role>(r => m.RoleName.Equals(r.RoleName)))
                .ToList<Core_Role>();
            db.core_role.RemoveRange(rolesToDelete);

            //commit db model changes and do the action in db
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