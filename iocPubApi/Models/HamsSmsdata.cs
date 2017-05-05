using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iocPubApi.Models
{
    [Table("HAMS_SMSData")]
    public partial class HamsSmsdata
    {
        public Guid DataId { get; set; }
        public int VehicleId { get; set; }
        public int? FileId { get; set; }
        [Column("CFGId")]
        public int? Cfgid { get; set; }
        [Column("GPS")]
        public int? Gps { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Lat { get; set; }
        [Column("SN", TypeName = "char(1)")]
        public string Sn { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Lng { get; set; }
        [Column("EW", TypeName = "char(1)")]
        public string Ew { get; set; }
        [Column(TypeName = "char(2)")]
        public string Accelerometer { get; set; }
        [Column(TypeName = "char(5)")]
        public string AxisX { get; set; }
        [Column(TypeName = "char(5)")]
        public string AxisY { get; set; }
        [Column(TypeName = "char(5)")]
        public string AxisZ { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RealTime { get; set; }
        public short IsUpdate { get; set; }
        public short? IsMoved { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateTime { get; set; }
    }
}
