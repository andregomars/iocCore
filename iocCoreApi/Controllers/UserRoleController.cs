﻿using System;
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
        public IQueryable<Core_UserRole> GetCore_UserRole()
        {
            return db.Core_UserRole;
        }

        // GET: api/UserRole/5
        [ResponseType(typeof(Core_UserRole))]
        public IHttpActionResult GetCore_UserRole(int id)
        {
            Core_UserRole Core_UserRole = db.Core_UserRole.Find(id);
            if (Core_UserRole == null)
            {
                return NotFound();
            }

            return Ok(Core_UserRole);
        }

        // PUT: api/UserRole/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCore_UserRole(int id, Core_UserRole Core_UserRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Core_UserRole.ID)
            {
                return BadRequest();
            }

            db.Entry(Core_UserRole).State = EntityState.Modified;

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
        public IHttpActionResult PostCore_UserRole(Core_UserRole Core_UserRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Core_UserRole.Add(Core_UserRole);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Core_UserRole.ID }, Core_UserRole);
        }

        // DELETE: api/UserRole/5
        [ResponseType(typeof(Core_UserRole))]
        public IHttpActionResult DeleteCore_UserRole(int id)
        {
            Core_UserRole Core_UserRole = db.Core_UserRole.Find(id);
            if (Core_UserRole == null)
            {
                return NotFound();
            }

            db.Core_UserRole.Remove(Core_UserRole);
            db.SaveChanges();

            return Ok(Core_UserRole);
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
            return db.Core_UserRole.Count(e => e.ID == id) > 0;
        }
    }
}