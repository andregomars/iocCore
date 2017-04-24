using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iocPubApi.Models
{
    [Table("IO_Fleet")]
    public partial class IoFleet
    {
        [Column("FleetID")]
        public int FleetId { get; set; }
        public int CompanyId { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string UserName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string VehicleType { get; set; }
        [Column("IntervalSMS")]
        public int? IntervalSms { get; set; }
        public int? IntervalNet { get; set; }
        public int? Timezone { get; set; }
        public int? TimeOffset { get; set; }
        public int? LogFormat { get; set; }
        [Column(TypeName = "varchar(8)")]
        public string LogStartTime { get; set; }
        [Column(TypeName = "varchar(8)")]
        public string LogEndTime { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Remark { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }
        public int? Status { get; set; }
    }
}
