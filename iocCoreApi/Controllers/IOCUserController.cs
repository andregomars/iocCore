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
        //[Route("api/AuthenticateUser")]
        //[ResponseType(typeof(Boolean))]
        //public bool Get(string loginName, string password)

        [ResponseType(typeof(IOC_User))]
        public IHttpActionResult GetIOC_User(string loginName, string password)
        {
            IOC_User user = new IOC_User();
            user.ID = 2;
            user.allcaps = new Allcaps() { administrator = true };
            user.cap_key = "ioc_capabilities";
            user.caps = new Caps() { administrator = true };
            user.data = new Data
            {
                ID = user.ID,
                deleted = 0,
                display_name = "carol liu",
                spam = 0,
                user_activation_key = "",
                user_email = "carol@gmail.com",
                user_login = "carol",
                user_nicename = "carol liu",
                //user_pass = "$P$BBh7gQRP.U9J3HOc03.hd4K0KFuShq0",
                user_pass = "1234",
                user_registered = "2016-06-05 00:16:33",
                user_status = 0,
                user_url = ""
            };
            user.filter = null;
            user.roles = new List<string>() { "administrator" };

            if (user.data.user_login == loginName && user.data.user_pass == password)
            {
                return Ok(user);
            }

            return NotFound();
        }
    }
}
