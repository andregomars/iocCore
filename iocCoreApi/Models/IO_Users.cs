namespace iocCoreApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IO_Users
    {
        [Key]
        public int UserId { get; set; }

        public int CompanyId { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(20)]
        public string LogName { get; set; }

        public int? Sex { get; set; }

        [StringLength(120)]
        public string Pwd { get; set; }

        [StringLength(20)]
        public string Tel { get; set; }

        [StringLength(60)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Mobile { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? LastTime { get; set; }

        public DateTime? ValidDate { get; set; }

        public int? UserType { get; set; }

        [StringLength(30)]
        public string HeadImage { get; set; }

        public int? IsActive { get; set; }

        public int? Status { get; set; }
    }
}
