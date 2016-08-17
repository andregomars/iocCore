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
    public class IO_UsersController : ApiController
    {
        private IO_OnlineDBModelsContext db = new IO_OnlineDBModelsContext();

        // GET: api/IO_Users
        public IQueryable<IO_Users> GetIO_Users()
        {
            return db.IO_Users;
        }

        // GET: api/IO_Users/5
        [ResponseType(typeof(IO_Users))]
        public IHttpActionResult GetIO_Users(int id)
        {
            IO_Users iO_Users = db.IO_Users.Find(id);
            if (iO_Users == null)
            {
                return NotFound();
            }

            return Ok(iO_Users);
        }

        // GET: api/User?loginName={loginName}
        [ResponseType(typeof(Core_User))]
        public IHttpActionResult GetIO_Users(string loginName)
        {
            IO_Users iO_Users = db.IO_Users.Where<IO_Users>(u => u.LogName == loginName).SingleOrDefault<IO_Users>();
            if (iO_Users == null)
            {
                return NotFound();
            }

            return Ok(iO_Users);
        }

        // PUT: api/IO_Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIO_Users(int id, IO_Users iO_Users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iO_Users.UserId)
            {
                return BadRequest();
            }

            db.Entry(iO_Users).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IO_UsersExists(id))
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

        // POST: api/IO_Users
        [ResponseType(typeof(IO_Users))]
        public IHttpActionResult PostIO_Users(IO_Users iO_Users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IO_Users.Add(iO_Users);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = iO_Users.UserId }, iO_Users);
        }

        // DELETE: api/IO_Users/5
        [ResponseType(typeof(IO_Users))]
        public IHttpActionResult DeleteIO_Users(int id)
        {
            IO_Users iO_Users = db.IO_Users.Find(id);
            if (iO_Users == null)
            {
                return NotFound();
            }

            db.IO_Users.Remove(iO_Users);
            db.SaveChanges();

            return Ok(iO_Users);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IO_UsersExists(int id)
        {
            return db.IO_Users.Count(e => e.UserId == id) > 0;
        }
    }
}