namespace iocCoreApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Core_SMS
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string MessageID { get; set; }

        [StringLength(50)]
        public string SubMessageID { get; set; }

        [StringLength(5)]
        public string SMSType { get; set; }

        [StringLength(50)]
        public string SenderCode { get; set; }

        [StringLength(50)]
        public string RecieverCode { get; set; }

        [StringLength(5)]
        public string Status { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? SendTime { get; set; }

        [StringLength(4000)]
        public string Message { get; set; }
    }
}
