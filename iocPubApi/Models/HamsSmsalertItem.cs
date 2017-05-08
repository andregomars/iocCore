using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iocPubApi.Models
{
    [Table("HAMS_SMSAlertItem")]
    public partial class HamsSmsalertItem
    {
        public Guid DataId { get; set; }
        [Column("SMSId")]
        public Guid Smsid { get; set; }
        public int VehicleId { get; set; }
        [Column(TypeName = "varchar(2)")]
        public string ItemCode { get; set; }
        [Column(TypeName = "varchar(16)")]
        public string Hex { get; set; }
        [Column(TypeName = "varchar(300)")]
        public string ItemName { get; set; }
        public long Data { get; set; }
        public double Value { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string TextStatus { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Unit { get; set; }
        public short? IsCondition { get; set; }
        public short IsUpdate { get; set; }
        [Column("PGN", TypeName = "varchar(6)")]
        public string Pgn { get; set; }
        [Column("SPN")]
        public short? Spn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateTime { get; set; }
    }
}
