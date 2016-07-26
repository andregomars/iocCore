namespace iocCoreApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Core_Permission
    {
        public int ID { get; set; }

        public int RoleID { get; set; }

        public int FunctionID { get; set; }

        public DateTime? InDate { get; set; }

        [StringLength(30)]
        public string InUser { get; set; }

        public DateTime? EditDate { get; set; }

        [StringLength(30)]
        public string EditUser { get; set; }
    }
}
