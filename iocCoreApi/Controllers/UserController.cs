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
        public IQueryable<Core_User> GetCore_user()
        {
            return db.Core_User;
        }

        // GET: api/User/5
        [ResponseType(typeof(Core_User))]
        public IHttpActionResult GetCore_user(int id)
        {
            Core_User Core_user = db.Core_User.Find(id);
            if (Core_user == null)
            {
                return NotFound();
            }

            return Ok(Core_user);
        }

        // GET: api/User?loginName={loginName}
        [ResponseType(typeof(Core_User))]
        public IHttpActionResult GetCore_user(string loginName)
        {
            Core_User Core_user = db.Core_User.Where<Core_User>(u=>u.LoginName == loginName).SingleOrDefault<Core_User>();
            if (Core_user == null)
            {
                return NotFound();
            }

            return Ok(Core_user);
        }

        // PUT: api/User/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCore_user(int id, Core_User Core_user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Core_user.ID)
            {
                return BadRequest();
            }

            db.Entry(Core_user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Core_userExists(id))
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
        [ResponseType(typeof(Core_User))]
        public IHttpActionResult PostCore_user(Core_User Core_user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Core_User.Add(Core_user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Core_user.ID }, Core_user);
        }

        // DELETE: api/User/5
        [ResponseType(typeof(Core_User))]
        public IHttpActionResult DeleteCore_user(int id)
        {
            Core_User Core_user = db.Core_User.Find(id);
            if (Core_user == null)
            {
                return NotFound();
            }

            db.Core_User.Remove(Core_user);
            db.SaveChanges();

            return Ok(Core_user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Core_userExists(int id)
        {
            return db.Core_User.Count(e => e.ID == id) > 0;
        }
    }
}