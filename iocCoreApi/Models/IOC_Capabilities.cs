using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iocCoreApi.Models
{
    public class IOC_Capabilities : IDictionary<string, bool>
    {
        private Dictionary<string, bool> capabilities;
        public IOC_Capabilities(Dictionary<string, bool> capabilities)
        {
            this.capabilities = capabilities;
        }

        public IOC_Capabilities()
        {
            this.capabilities = new Dictionary<string, bool>();
        }

        public bool this[string key]
        {
            get
            {
                return capabilities[key];
            }

            set
            {
                capabilities[key] = value;
            }
        }

        public int Count
        {
            get
            {
                return capabilities.Count;
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
                return capabilities.Keys;
            }
        }

        public ICollection<bool> Values
        {
            get
            {
                return capabilities.Values;
            }
        }

        public void Add(KeyValuePair<string, bool> item)
        {
            this.Add(item.Key, item.Value);
        }

        public void Add(string key, bool value)
        {
            capabilities.Add(key, value);
        }

        public void Clear()
        {
            capabilities.Clear();
        }

        public bool Contains(KeyValuePair<string, bool> item)
        {
            return capabilities.Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return capabilities.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, bool>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, bool>> GetEnumerator()
        {
            return capabilities.GetEnumerator();
        }

        public bool Remove(KeyValuePair<string, bool> item)
        {
            return this.Remove(item.Key);
        }

        public bool Remove(string key)
        {
            return capabilities.Remove(key);
        }

        public bool TryGetValue(string key, out bool value)
        {
            return capabilities.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this.GetEnumerator();
        }
    }
}