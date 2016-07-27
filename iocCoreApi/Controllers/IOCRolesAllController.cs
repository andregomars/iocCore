using iocCoreApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;
using System.Web.Http.Description;

namespace iocCoreApi.Controllers
{
    public class IOCRolesAllController : ApiController
    {
        private CoreDBModelsContext db = new CoreDBModelsContext();

        [ResponseType(typeof(Dictionary<String, JsonRole>))]
        public IHttpActionResult GetIOC_RolesAll()
        {
            var roles = (from role in db.core_role
                        select new IOC_RolePermission
                        {
                            name = role.RoleName,
                            capabilities = (from func in db.core_Function
                                            join perm in db.core_Permission
                                            on func.ID equals perm.FunctionID
                                            where perm.RoleID == role.ID
                                            select new IOC_Capability
                                            {
                                                Name = func.FunctionName.ToLower(),
                                                Enabled = true
                                            }
                                            ).ToList<IOC_Capability>()
                        }).ToList<IOC_RolePermission>();

            //convert c# obj to json associate arrays
            Dictionary<String, JsonRole> allRolesInJson = new Dictionary<String, JsonRole>();
            foreach (IOC_RolePermission role in roles)
            {
                JsonRole roleInJson = new JsonRole();
                roleInJson.name = role.name;
                roleInJson.capabilities = new Dictionary<string, bool>();
                foreach (IOC_Capability cap in role.capabilities)
                {
                    roleInJson.capabilities.Add(cap.Name.ToLower(), cap.Enabled);
                }
                allRolesInJson.Add(role.name.ToLower(), roleInJson);
            }

            if (allRolesInJson == null)
            {
                return NotFound();
            }

            return Ok(allRolesInJson);
        }

        [DataContract]
        class JsonRole
        {
            [DataMember]
            public string name { get; set; }

            [DataMember]
            public Dictionary<String, Boolean> capabilities { get; set; }
        }
    }




}
