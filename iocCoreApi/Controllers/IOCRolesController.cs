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
    public class IOCRolesController : ApiController
    {
        private CoreDBModelsContext db = new CoreDBModelsContext();

        [ResponseType(typeof(IOC_Roles))]
        public IHttpActionResult GetIOC_Roles()
        {
            IOC_Roles ioc_roles = new IOC_Roles();

            var query = from role in db.core_role
                       //select role.RoleName;
                        select new
                        {
                            name = role.RoleName,
                            capabilities = ""
                        };


            //foreach (var role in query)
            //{
            //    ioc_roles.Add(
            //        new IOC_Role{ name= role.name, capabilities = role.capabilities});
            //}

            if (ioc_roles == null)
            {
                return NotFound();
            }

            return Ok(ioc_roles);
        }
    }
}
