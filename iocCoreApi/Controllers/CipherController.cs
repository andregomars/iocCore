using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using iocCoreApi.Services;

namespace iocCoreApi.Controllers
{
    public class CipherController : ApiController
    {
        [Route("api/Cipher/User")]
        public IHttpActionResult Get(string UserID)
        {
            if (String.IsNullOrEmpty(UserID) || String.IsNullOrEmpty(password))
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
