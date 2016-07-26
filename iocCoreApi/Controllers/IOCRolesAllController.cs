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
    public class IOCRolesAllController : ApiController
    {
        private CoreDBModelsContext db = new CoreDBModelsContext();

        [ResponseType(typeof(Dictionary<String, JsonRole>))]
        public IHttpActionResult GetIOC_Roles()
        {
            IOC_Roles ioc_roles = new IOC_Roles();

            var query = from role in db.core_role
                        select new IOC_Role
                        {
                            name = role.RoleName,
                            capabilities = (from func in db.core_Function
                                            join perm in db.core_Permission
                                            on func.ID equals perm.FunctionID
                                            where perm.RoleID == role.ID
                                            select func.FunctionName).ToList<String>()
                        };

            IEnumerable<IOC_Role> roles = query.ToList<IOC_Role>();

            Dictionary<String, JsonRole> jsonRoles = new Dictionary<String, JsonRole>();
            foreach (var role in roles)
            {
                JsonRole jsonRole = new JsonRole();
                jsonRole.name = role.name;
                jsonRole.capabilities = new Dictionary<string, bool>();
                foreach (String cap in role.capabilities)
                {
                    jsonRole.capabilities.Add(cap.ToLower(), true);
                }
                jsonRoles.Add(role.name.ToLower(), jsonRole);
            }

            if (jsonRoles == null)
            {
                return NotFound();
            }

            return Ok(jsonRoles);
        }
    }


    public class JsonRole
    {
        public string name { get; set; }
        public Dictionary<String, Boolean> capabilities { get; set; }
    }

}
