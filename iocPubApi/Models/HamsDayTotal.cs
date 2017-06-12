using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iocPubApi.Models
{
    [Table("HAMS_DayTotal")]
    public partial class HamsDayTotal
    {
        [Column("YYMMDD", TypeName = "varchar(6)")]
        public string Yymmdd { get; set; }
        public int VehicleId { get; set; }
        public short? DayHours { get; set; }
        public double? Mileage { get; set; }
        [Column("SOC_Charged")]
        public double? SocCharged { get; set; }
        [Column("SOC_Used")]
        public double? SocUsed { get; set; }
        [Column("kWh_Charged")]
        public double? KWhCharged { get; set; }
        [Column("kWh_Used")]
        public double? KWhUsed { get; set; }
        [Column("Hight_Voltage")]
        public double? HightVoltage { get; set; }
        [Column("Low_Voltage")]
        public double? LowVoltage { get; set; }
        [Column("Hight_Tmp")]
        public double? HightTmp { get; set; }
        [Column("Low_Tmp")]
        public double? LowTmp { get; set; }
        [Column("Hight_Current")]
        public double? HightCurrent { get; set; }
        [Column("Low_Current")]
        public double? LowCurrent { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RealTime { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string DataType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateTime { get; set; }
    }
}
