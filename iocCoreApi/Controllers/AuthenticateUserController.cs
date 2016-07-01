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
    public class AuthenticateUserController : ApiController
    {
        private CoreDBModelsContext db = new CoreDBModelsContext();

        [Route("api/AuthenticateUser")]
        [ResponseType(typeof(Boolean))]
        public bool Get(string loginName, string password)
        {
            if (String.IsNullOrEmpty(loginName) || String.IsNullOrEmpty(password))
                return false;

            var users = from user in db.core_user
                        where user.LoginName == loginName && user.Password == password
                        select user;

            return users.Any();
        }
    }
}
