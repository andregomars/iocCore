using iocCoreApi.Models;
using iocCoreApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace iocCoreApi.Controllers
{
    public class AuthenticateUserController : ApiController
    {
        private CoreDBModelsContext db = new CoreDBModelsContext();

        [Route("api/AuthenticateUser")]
        [ResponseType(typeof(Boolean))]
        public IHttpActionResult Get(string loginName, string password)
        {
            if (String.IsNullOrEmpty(loginName) || String.IsNullOrEmpty(password))
                return BadRequest("loginName and password are both required.");

            Core_User coreUser = (from user in db.Core_User
                                  where user.LoginName == loginName
                              select user).SingleOrDefault<Core_User>();
            if (coreUser == null)
            {
                return NotFound();
            }

            bool isValidUser = Utility.VerifyTripleMd5Hash(password, coreUser.Password);

            if (isValidUser)
            {
                return Ok(coreUser);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
