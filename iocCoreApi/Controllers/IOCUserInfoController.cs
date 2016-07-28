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
    public class IOCUserInfoController : ApiController
    {
        private CoreDBModelsContext db = new CoreDBModelsContext();

        [ResponseType(typeof(IOC_UserInfo))]
        public IHttpActionResult GetIOC_UserInfo(string loginName)
        {
            Dictionary<string, bool> allCaps = new Dictionary<string, bool>();
            Dictionary<string, bool> caps = new Dictionary<string, bool>();
            List<string> roles;
            IOC_User iocUser;

            //get user data
            List<Core_User> users = (from iocuser in db.core_user
                                     where iocuser.LoginName == loginName
                                     select iocuser).ToList<Core_User>();

            if (users == null || users.Count < 1)
            {
                return NotFound();
            }

            iocUser = new IOC_User(users[0].ID, users[0].Name, users[0].Email,
                users[0].LoginName, users[0].Name, users[0].Password, users[0].InDate, users[0].Status);

            //get allCaps array
            var funcs = from func in db.core_Function
                        join perm in db.core_Permission
                            on func.ID equals perm.FunctionID
                        join userRole in db.core_UserRole
                            on perm.RoleID equals userRole.RoleID
                        join user2 in db.core_user
                            on userRole.UserID equals user2.ID
                        where user2.LoginName == loginName
                        select func.FunctionName;

            foreach (string func in funcs.Distinct().ToList<string>())
            {
                allCaps.Add(func, true);
            }

            //get caps and roles array
            var coreRoles = from user3 in db.core_user
                        join userRole in db.core_UserRole
                            on user3.ID equals userRole.UserID
                        join role in db.core_role
                            on userRole.RoleID equals role.ID
                        where user3.LoginName == loginName
                        select role.RoleName.ToLower();

            roles = coreRoles.ToList<string>();
            foreach (string role in roles)
            {
                caps.Add(role.ToLower(), true);
            }

            IOC_UserInfo userInfo = new IOC_UserInfo(iocUser.ID, allCaps, caps, iocUser, roles);

            if (userInfo != null)
            {
                return Ok(userInfo);
            }

            return NotFound();

        }

    }

    /*
     {
    "ID": 3,
    "allcaps": {
        "activate_plugins": 1,
        "add_users": 1,
        "administrator": 1,
        "create_users": 1,
        "delete_others_pages": 1,
        "delete_others_posts": 1,
        "delete_pages": 1,
        "delete_plugins": 1,
        "delete_posts": 1,
        "delete_private_pages": 1,
        "delete_private_posts": 1,
        "delete_published_pages": 1,
        "delete_published_posts": 1,
        "delete_themes": 1,
        "delete_users": 1,
        "edit_dashboard": 1,
        "edit_files": 1,
        "edit_others_pages": 1,
        "edit_others_posts": 1,
        "edit_pages": 1,
        "edit_plugins": 1,
        "edit_posts": 1,
        "edit_private_pages": 1,
        "edit_private_posts": 1,
        "edit_published_pages": 1,
        "edit_published_posts": 1,
        "edit_theme_options": 1,
        "edit_themes": 1,
        "edit_users": 1,
        "export": 1,
        "import": 1,
        "install_plugins": 1,
        "install_themes": 1,
        "level_0": 1,
        "level_1": 1,
        "level_10": 1,
        "level_2": 1,
        "level_3": 1,
        "level_4": 1,
        "level_5": 1,
        "level_6": 1,
        "level_7": 1,
        "level_8": 1,
        "level_9": 1,
        "list_users": 1,
        "manage_categories": 1,
        "manage_links": 1,
        "manage_options": 1,
        "moderate_comments": 1,
        "promote_users": 1,
        "publish_pages": 1,
        "publish_posts": 1,
        "read": 1,
        "read_private_pages": 1,
        "read_private_posts": 1,
        "remove_users": 1,
        "switch_themes": 1,
        "unfiltered_html": 1,
        "unfiltered_upload": 1,
        "update_core": 1,
        "update_plugins": 1,
        "update_themes": 1,
        "upload_files": 1
    },
    "cap_key": "ioc_capabilities",
    "caps": {
        "administrator": 1
    },
    "data": {
        "ID": 3,
        "deleted": false,
        "display_name": "ioc dbo",
        "spam": false,
        "user_activation_key": "",
        "user_email": "iocdbo@gmail.com",
        "user_login": "iocdbo",
        "user_nicename": "ioc dbo",
        "user_pass": "8be4177df4ec5dee8c8bc4f3b49d7a2d",
        "user_registered": "2016-06-30T22:51:34.293",
        "user_status": true,
        "user_url": ""
    },
    "filter": "",
    "roles": [
        "administrator"
    ]
    }
     */
}
