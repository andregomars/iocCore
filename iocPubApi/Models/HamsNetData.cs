using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iocPubApi.Models
{
    [Table("HAMS_NetData")]
    public partial class HamsNetData
    {
        public Guid DataId { get; set; }
        public int VehicleId { get; set; }
        [Column("MAC", TypeName = "varchar(50)")]
        public string Mac { get; set; }
        [Column("GPS")]
        public short? Gps { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Lng { get; set; }
        [Column("EW", TypeName = "char(1)")]
        public string Ew { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Lat { get; set; }
        [Column("SN", TypeName = "char(1)")]
        public string Sn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataTime { get; set; }
        public short? DataType { get; set; }
        [MaxLength(1024)]
        public byte[] DataArray { get; set; }
        public short? IsView { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RealTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateTime { get; set; }
    }
}
