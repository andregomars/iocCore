using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iocCoreApi.Models
{
    //public class IOC_Roles : IEnumerable<IOC_Role>
    public class IOC_Roles
    {
        public List<IOC_Role> Roles { get; set; }

        public IOC_Roles()
        {
            Roles = new List<IOC_Role>();
        }

        //public IOC_Role this[int index]
        //{
        //    get { return Roles[index]; }
        //    set { Roles.Insert(index, value); }
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return Roles.GetEnumerator();
        //}

        //IEnumerator<IOC_Role> IEnumerable<IOC_Role>.GetEnumerator()
        //{
        //    return Roles.GetEnumerator();
        //}
    }
}