using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iocPubApi.Models
{
    [Table("HAMS_AlertItem")]
    public partial class HamsAlertItem
    {
        public int Id { get; set; }
        public Guid DataId { get; set; }
        [Required]
        [Column(TypeName = "varchar(2)")]
        public string ItemCode { get; set; }
        [Column(TypeName = "varchar(300)")]
        public string ItemName { get; set; }
        public long? Data { get; set; }
        public double Value { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Unit { get; set; }
        public short? IsSource { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Remark { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateTime { get; set; }
    }
}
