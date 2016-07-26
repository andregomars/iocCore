namespace iocCoreApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Core_Function
    {
        public int ID { get; set; }

        public int? MenuID { get; set; }

        [Required]
        [StringLength(100)]
        public string FunctionName { get; set; }

        [StringLength(500)]
        public string FunctionDescription { get; set; }

        [Required]
        [StringLength(20)]
        public string FunctionType { get; set; }

        public int? Priority { get; set; }

        public DateTime? InDate { get; set; }

        [StringLength(30)]
        public string InUser { get; set; }

        public DateTime? EditDate { get; set; }

        [StringLength(30)]
        public string EditUser { get; set; }
    }
}
