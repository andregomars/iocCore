namespace iocCoreApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class core_user
    {
        public int ID { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(30)]
        public string LoginName { get; set; }

        [Required]
        [StringLength(20)]
        public string UserType { get; set; }

        public int CompanyID { get; set; }

        public int IsAdmin { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(20)]
        public string CellPhone { get; set; }

        [StringLength(20)]
        public string WorkPhone { get; set; }

        [StringLength(60)]
        public string Email { get; set; }

        [StringLength(500)]
        public string HeadImage { get; set; }

        public DateTime? InDate { get; set; }

        [StringLength(30)]
        public string InUser { get; set; }

        public DateTime? EditDate { get; set; }

        [StringLength(30)]
        public string EditUser { get; set; }

        [StringLength(20)]
        public string Status { get; set; }
    }
}
