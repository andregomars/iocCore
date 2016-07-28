using iocCoreApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace iocCoreApi.Controllers
{
    public class IOCUserController : ApiController
    {
        private CoreDBModelsContext db = new CoreDBModelsContext();

        // api/IOCUser?loginName={loginName}
        [ResponseType(typeof(IOC_User))]
        public IHttpActionResult GetIOC_User(string loginName)
        {
            if (String.IsNullOrWhiteSpace(loginName)) return NotFound();

            List<Core_User> users = (from user in db.core_user
                                     where user.LoginName == loginName
                                     select user).ToList<Core_User>();
            
            if (users == null || users.Count < 1)
            {
                return NotFound();
            }

            IOC_User iocUser = new IOC_User(users[0].ID, users[0].Name, users[0].Email,
                users[0].LoginName, users[0].Name, users[0].Password, users[0].InDate, users[0].Status);

            return Ok(iocUser);
        }

        [ResponseType(typeof(IOC_User))]
        [Route("api/IOCUser/{id}")]
        public IHttpActionResult GetIOC_User(int id)
        {
            if (id < 0) return NotFound();

            Core_User user = db.core_user.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            IOC_User iocUser = new IOC_User(user.ID, user.Name, user.Email,
                user.LoginName, user.Name, user.Password, user.InDate, user.Status);

            return Ok(iocUser);
        }
    }
}
