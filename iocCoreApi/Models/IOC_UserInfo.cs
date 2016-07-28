using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iocCoreApi.Models
{
    public class IOC_UserInfo
    {
        public int ID { get; set; }
        public Dictionary<string, bool> allcaps { get; set; }
        public string cap_key { get; set; }
        public Dictionary<string, bool> caps { get; set; }
        public IOC_User data { get; set; }
        public string filter { get; set; }
        public List<string> roles { get; set; }

        public IOC_UserInfo(int id, Dictionary<string, bool> allcaps,
            Dictionary<string, bool> caps, IOC_User data, List<string> roles)
        {
            this.ID = id;
            this.allcaps = allcaps;
            this.cap_key = "ioc_capabilities";
            this.caps = caps;
            this.data = data;
            this.filter = "";
            this.roles = roles;
        }
    }

}