namespace iocCoreApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IO_Company
    {
        [Key]
        public int CompanyId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(60)]
        public string Address { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string ZipCode { get; set; }

        [StringLength(50)]
        public string Fax { get; set; }

        [StringLength(50)]
        public string Tel { get; set; }

        [StringLength(50)]
        public string Relation { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        public int? CompanyType { get; set; }

        public int? IsStop { get; set; }

        public DateTime? CreateTime { get; set; }

        public int? Status { get; set; }
    }
}
