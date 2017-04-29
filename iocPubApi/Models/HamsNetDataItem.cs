using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iocPubApi.Models
{
    [Table("HAMS_NetDataItem")]
    public partial class HamsNetDataItem
    {
        public int Id { get; set; }
        public Guid DataId { get; set; }
        public int DataType { get; set; }
        [Column("CanID")]
        public int? CanId { get; set; }
        [MaxLength(8)]
        public byte[] Data { get; set; }
        [Column("PGN", TypeName = "varchar(4)")]
        public string Pgn { get; set; }
        [Column("SPN")]
        public int? Spn { get; set; }
        [Column("SPNName", TypeName = "varchar(300)")]
        public string Spnname { get; set; }
        public int? Source { get; set; }
        public double Value { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string StatusText { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string Unit { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateTime { get; set; }
    }
}
