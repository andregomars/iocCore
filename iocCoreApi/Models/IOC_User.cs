using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iocCoreApi.Models
{
    public class IOC_User
    {
        public int ID { get; set; }
        public bool deleted { get; set; }
        public string display_name { get; set; }
        public bool spam { get; set; }
        public string user_activation_key { get; set; }
        public string user_email { get; set; }
        public string user_login { get; set; }
        public string user_nicename { get; set; }
        public string user_pass { get; set; }
        public DateTime user_registered { get; set; }
        public bool user_status { get; set; }
        public string user_url { get; set; }

        public IOC_User()
        { }

        public IOC_User(int id, string display_name, string user_email, string user_login,
            string user_nicename, string user_pass, DateTime? user_registered, string user_status)
        {
            this.ID = id;
            this.deleted = false;
            this.display_name = display_name;
            this.spam = false;
            this.user_activation_key = "";
            this.user_email = user_email;
            this.user_login = user_login;
            this.user_nicename = user_nicename;
            this.user_pass = user_pass;
            this.user_registered = user_registered == null ? DateTime.Now : user_registered.Value;
            this.user_status = user_status.Equals("Active");
            this.user_url = "";
        }
    }

    /*
    {
        "ID": 5,
        "deleted": 0,
        "display_name": "iocdbo",
        "spam": 0,
        "user_activation_key": "",
        "user_email": "andregomars@gmail.com",
        "user_login": "iocdbo",
        "user_nicename": "iocdbo",
        "user_pass": "8be4177df4ec5dee8c8bc4f3b49d7a2d",
        "user_registered": "2016-06-05 00:16:33",
        "user_status": 0,
        "user_url": ""
    }
     */
}
 