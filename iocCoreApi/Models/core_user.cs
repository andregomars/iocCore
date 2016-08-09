namespace iocCoreApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Core_User
    {
        public int ID { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        public string LoginName { get; set; }

        public int? UserType { get; set; }

        public int CompanyID { get; set; }

        public int? Gender { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(20)]
        public string Tel { get; set; }

        [StringLength(20)]
        public string Mobile { get; set; }

        [StringLength(60)]
        public string Email { get; set; }

        [StringLength(500)]
        public string HeadImage { get; set; }

        public DateTime? ValidDate { get; set; }

        public int? IsActive { get; set; }

        public DateTime? InDate { get; set; }

        [StringLength(30)]
        public string InUser { get; set; }

        public DateTime? EditDate { get; set; }

        [StringLength(30)]
        public string EditUser { get; set; }

        public int? Status { get; set; }
    }
}
