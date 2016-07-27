using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iocCoreApi.Models
{
    public class IOC_RolePermission
    {
        public string name { get; set; }
        public List<IOC_Capability> capabilities { get; set; }
    }
}