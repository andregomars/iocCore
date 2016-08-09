namespace iocCoreApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Core_Company
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(20)]
        public string CompanyType { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string Fax { get; set; }

        [StringLength(20)]
        public string Tel { get; set; }

        [StringLength(20)]
        public string Mobile { get; set; }

        [StringLength(50)]
        public string Relation { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int? IsStop { get; set; }

        public DateTime? InDate { get; set; }

        [StringLength(30)]
        public string InUser { get; set; }

        public DateTime? EditDate { get; set; }

        [StringLength(30)]
        public string EditUser { get; set; }

        public int? Status { get; set; }
    }
}
