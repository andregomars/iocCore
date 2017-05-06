using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iocPubApi.Models
{
    [Table("HAMS_AlertData")]
    public partial class HamsAlertData
    {
        public Guid DataId { get; set; }
        public Guid? LinkId { get; set; }
        public int VehicleId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataTime { get; set; }
        [MaxLength(1024)]
        public byte[] DataArray { get; set; }
        public short? IsView { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Viewer { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ViewTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RealTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateTime { get; set; }
    }
}
