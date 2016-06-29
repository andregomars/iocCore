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
    public class UserController : ApiController
    {
        private CoreDBModelsContext db = new CoreDBModelsContext();

        // GET: api/User
        public IQueryable<core_user> Getcore_user()
        {
            return db.core_user;
        }

        // GET: api/User/5
        [ResponseType(typeof(core_user))]
        public IHttpActionResult Getcore_user(int id)
        {
            core_user core_user = db.core_user.Find(id);
            if (core_user == null)
            {
                return NotFound();
            }

            return Ok(core_user);
        }

        // PUT: api/User/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcore_user(int id, core_user core_user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != core_user.ID)
            {
                return BadRequest();
            }

            db.Entry(core_user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!core_userExists(id))
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

        // POST: api/User
        [ResponseType(typeof(core_user))]
        public IHttpActionResult Postcore_user(core_user core_user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.core_user.Add(core_user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = core_user.ID }, core_user);
        }

        // DELETE: api/User/5
        [ResponseType(typeof(core_user))]
        public IHttpActionResult Deletecore_user(int id)
        {
            core_user core_user = db.core_user.Find(id);
            if (core_user == null)
            {
                return NotFound();
            }

            db.core_user.Remove(core_user);
            db.SaveChanges();

            return Ok(core_user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool core_userExists(int id)
        {
            return db.core_user.Count(e => e.ID == id) > 0;
        }
    }
}