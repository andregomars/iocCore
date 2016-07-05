using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iocCoreApi.Models
{
    public class IOC_User
    {
        public int ID { get; set; }
        public Allcaps allcaps { get; set; }
        public string cap_key { get; set; }
        public Caps caps { get; set; }
        public Data data { get; set; }
        public object filter { get; set; }
        public List<string> roles { get; set; }
    }

    public class Allcaps
    {
        public bool activate_plugins { get; set; }
        public bool add_users { get; set; }
        public bool administrator { get; set; }
        public bool create_users { get; set; }
        public bool delete_others_pages { get; set; }
        public bool delete_others_posts { get; set; }
        public bool delete_pages { get; set; }
        public bool delete_plugins { get; set; }
        public bool delete_posts { get; set; }
        public bool delete_private_pages { get; set; }
        public bool delete_private_posts { get; set; }
        public bool delete_published_pages { get; set; }
        public bool delete_published_posts { get; set; }
        public bool delete_themes { get; set; }
        public bool delete_users { get; set; }
        public bool edit_dashboard { get; set; }
        public bool edit_files { get; set; }
        public bool edit_others_pages { get; set; }
        public bool edit_others_posts { get; set; }
        public bool edit_pages { get; set; }
        public bool edit_plugins { get; set; }
        public bool edit_posts { get; set; }
        public bool edit_private_pages { get; set; }
        public bool edit_private_posts { get; set; }
        public bool edit_published_pages { get; set; }
        public bool edit_published_posts { get; set; }
        public bool edit_theme_options { get; set; }
        public bool edit_themes { get; set; }
        public bool edit_users { get; set; }
        public bool export { get; set; }
        public bool import { get; set; }
        public bool install_plugins { get; set; }
        public bool install_themes { get; set; }
        public bool level_0 { get; set; }
        public bool level_1 { get; set; }
        public bool level_10 { get; set; }
        public bool level_2 { get; set; }
        public bool level_3 { get; set; }
        public bool level_4 { get; set; }
        public bool level_5 { get; set; }
        public bool level_6 { get; set; }
        public bool level_7 { get; set; }
        public bool level_8 { get; set; }
        public bool level_9 { get; set; }
        public bool list_users { get; set; }
        public bool manage_categories { get; set; }
        public bool manage_links { get; set; }
        public bool manage_options { get; set; }
        public bool moderate_comments { get; set; }
        public bool promote_users { get; set; }
        public bool publish_pages { get; set; }
        public bool publish_posts { get; set; }
        public bool read { get; set; }
        public bool read_private_pages { get; set; }
        public bool read_private_posts { get; set; }
        public bool remove_users { get; set; }
        public bool switch_themes { get; set; }
        public bool unfiltered_html { get; set; }
        public bool unfiltered_upload { get; set; }
        public bool update_core { get; set; }
        public bool update_plugins { get; set; }
        public bool update_themes { get; set; }
        public bool upload_files { get; set; }
    }

    public class Caps
    {
        public bool administrator { get; set; }

        public static implicit operator Caps(Allcaps v)
        {
            throw new NotImplementedException();
        }
    }

    public class Data
    {
        public int ID { get; set; }
        public int deleted { get; set; }
        public string display_name { get; set; }
        public int spam { get; set; }
        public string user_activation_key { get; set; }
        public string user_email { get; set; }
        public string user_login { get; set; }
        public string user_nicename { get; set; }
        public string user_pass { get; set; }
        public string user_registered { get; set; }
        public int user_status { get; set; }
        public string user_url { get; set; }
    }



}