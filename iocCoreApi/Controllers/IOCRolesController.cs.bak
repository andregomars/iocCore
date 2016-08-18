using iocCoreApi.Areas.HelpPage.ModelDescriptions;
using iocCoreApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;
using System.Web.Http.Description;

namespace iocCoreApi.Controllers
{
    public class IOCRolesController : ApiController
    {
        private CoreDBModelsContext db = new CoreDBModelsContext();

        //[ResponseType(typeof(Roles))]
        [ResponseType(typeof(IDictionary<string, Role>))]
        public IHttpActionResult GetIOC_Roles()
        {
            Roles iocRoles = new Roles();
            iocRoles.Add("administrator", new Role { Name = "Administrator", ID = 1 });
            iocRoles.Add("editor", new Role { Name = "Editor", ID = 2 });
            iocRoles.Add("subscriber", new Role { Name = "Subscriber", ID = 3 });

            if (iocRoles == null)
            {
                return NotFound();
            }

            return Ok(iocRoles);
        }
    }

    //[ModelName("Roles")]
    //[DataContract]
    public class Roles : IDictionary<string, Role>
    {
        private Dictionary<string, Role> roles;
        public Roles(Dictionary<string, Role> roles)
        {
            this.roles = roles;
        }

        public Roles()
        {
            this.roles = new Dictionary<string, Role>();
        }

        public Role this[string key]
        {
            get
            {
                return roles[key];
            }

            set
            {
                roles[key] = value;
            }
        }

        public int Count
        {
            get
            {
                return roles.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public ICollection<string> Keys
        {
            get
            {
                return roles.Keys;
            }
        }

        public ICollection<Role> Values
        {
            get
            {
                return roles.Values;
            }
        }

        public void Add(KeyValuePair<string, Role> item)
        {
            this.Add(item.Key, item.Value);
        }

        public void Add(string key, Role value)
        {
            roles.Add(key, value);
        }

        public void Clear()
        {
            roles.Clear();
        }

        public bool Contains(KeyValuePair<string, Role> item)
        {
            return roles.Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return roles.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, Role>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, Role>> GetEnumerator()
        {
            return roles.GetEnumerator();
        }

        public bool Remove(KeyValuePair<string, Role> item)
        {
            return this.Remove(item.Key);
        }

        public bool Remove(string key)
        {
            return roles.Remove(key);
        }

        public bool TryGetValue(string key, out Role value)
        {
            return roles.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this.GetEnumerator();
        }
    }
    
    public class Role
    {
        public string Name { get; set; }
        public int ID { get; set; }
    }

}
