namespace iocCoreApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Core_Role
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        [Required]
        [StringLength(20)]
        public string RoleType { get; set; }

        [StringLength(100)]
        public string RoleDescription { get; set; }

        public DateTime? InDate { get; set; }

        [StringLength(30)]
        public string InUser { get; set; }

        public DateTime? EditDate { get; set; }

        [StringLength(30)]
        public string EditUser { get; set; }
    }
}
