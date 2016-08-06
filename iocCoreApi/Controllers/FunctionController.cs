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
    public class FunctionController : ApiController
    {
        private CoreDBModelsContext db = new CoreDBModelsContext();

        // GET: api/Function
        public IQueryable<Core_Function> Getcore_Function()
        {
            return db.core_Function;
        }

        // GET: api/Function/5
        [ResponseType(typeof(Core_Function))]
        public IHttpActionResult GetCore_Function(int id)
        {
            Core_Function core_Function = db.core_Function.Find(id);
            if (core_Function == null)
            {
                return NotFound();
            }

            return Ok(core_Function);
        }

        // PUT: api/Function/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCore_Function(int id, Core_Function core_Function)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != core_Function.ID)
            {
                return BadRequest();
            }

            db.Entry(core_Function).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Core_FunctionExists(id))
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

        // POST: api/Function
        [ResponseType(typeof(Core_Function))]
        public IHttpActionResult PostCore_Function(Core_Function core_Function)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.core_Function.Add(core_Function);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = core_Function.ID }, core_Function);
        }

        // POST: api/Function/Batch
        [ResponseType(typeof(List<Core_Function>))]
        [Route("api/Function/Batch")]
        public IHttpActionResult PostCore_Roles(List<Core_Function> core_Functions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Core_Function> masterFunctions = db.core_Function.ToList<Core_Function>();
            
            //find out functions to update, and mark them in db model to modified
            List<Core_Function> functionsToUpdate =
                masterFunctions.Where<Core_Function>(m => core_Functions.Any<Core_Function>(r => m.FunctionName.Equals(r.FunctionName)))
                .ToList<Core_Function>();
            functionsToUpdate.ForEach(m =>
            {
                Core_Function row = core_Functions.Find(r => m.FunctionName.Equals(r.FunctionName));
                m.FunctionType = row.FunctionType;
                m.FunctionDescription = row.FunctionDescription;
                m.EditDate = DateTime.Now;
                m.EditUser = row.EditUser;
            });
            functionsToUpdate.ForEach(m => db.Entry(m).State = EntityState.Modified);

            //find out functions to insert, and insert them into db model
            List<Core_Function> functionsToInsert =
                core_Functions.Where<Core_Function>(r => !masterFunctions.Any<Core_Function>(m => m.FunctionName.Equals(r.FunctionName)))
                .ToList<Core_Function>();
            db.core_Function.AddRange(functionsToInsert);

            //find out functions to delete, and remove them from db model
            List<Core_Function> functionsToDelete =
                masterFunctions.Where<Core_Function>(m => !core_Functions.Any<Core_Function>(r => m.FunctionName.Equals(r.FunctionName)))
                .ToList<Core_Function>();
            db.core_Function.RemoveRange(functionsToDelete);

            //commit db model changes and do the action in db
            db.SaveChanges();

            return Ok(db.core_Function);
        }


        // DELETE: api/Function/Batch
        [ResponseType(typeof(void))]
        [Route("api/Function/Batch")]
        public IHttpActionResult DeleteCore_Functions([FromBody] int[] ids)
        {
            Core_Function[] core_Functions = db.core_Function
                .Where<Core_Function>(r=>ids.Contains<int>( r.ID ))
                .ToArray<Core_Function>();

            if (core_Functions == null || core_Functions.Length == 0)
            {
                return NotFound();
            }

            db.core_Function.RemoveRange(core_Functions);
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

        private bool Core_FunctionExists(int id)
        {
            return db.core_Function.Count(e => e.ID == id) > 0;
        }


        /*
                // POST: api/Function/Batch
                [ResponseType(typeof(Core_Function[]))]
                [Route("api/Function/Batch")]
                public IHttpActionResult PostCore_Functions([FromBody] Core_Function[] core_Functions)
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    db.core_Function.AddRange(core_Functions);
                    db.SaveChanges();

                    return Ok(db.core_Function);
                }

                // DELETE: api/Function/5
                [ResponseType(typeof(Core_Function))]
                public IHttpActionResult DeleteCore_Function(int id)
                {
                    Core_Function core_Function = db.core_Function.Find(id);
                    if (core_Function == null)
                    {
                        return NotFound();
                    }

                    db.core_Function.Remove(core_Function);
                    db.SaveChanges();

                    return Ok(core_Function);
                }
        */
    }
}