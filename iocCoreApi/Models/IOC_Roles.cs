using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iocCoreApi.Models
{
    public class IOC_Roles : IEnumerable<IOC_Role>
    {
        List<IOC_Role> roles = new List<IOC_Role>();

        public IOC_Role this[int index]
        {
            get { return roles[index]; }
            set { roles.Insert(index, value); }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return roles.GetEnumerator();
        }

        IEnumerator<IOC_Role> IEnumerable<IOC_Role>.GetEnumerator()
        {
            return roles.GetEnumerator();
        }
    }
}