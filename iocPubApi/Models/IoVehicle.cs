using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iocPubApi.Models
{
    [Table("IO_Vehicle")]
    public partial class IoVehicle
    {
        public int VehicleId { get; set; }
        public int? FleetId { get; set; }
        public int CompanyId { get; set; }
        public int? BuilderId { get; set; }
        public int UserId { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string BusNo { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Phone { get; set; }
        [Required]
        [Column("MAC", TypeName = "varchar(50)")]
        public string Mac { get; set; }
        [Column("VINNO", TypeName = "varchar(30)")]
        public string Vinno { get; set; }
        public short? TypeId { get; set; }
        public short Online { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? OnlineTime { get; set; }
        public short? Monitor { get; set; }
        public short? Renewable { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Remark { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }
        public int? Status { get; set; }
    }
}
