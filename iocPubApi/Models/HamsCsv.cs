using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iocPubApi.Models
{
    [Table("HAMS_CSV")]
    public partial class HamsCsv
    {
        [Column("CSVId")]
        public long Csvid { get; set; }
        public int VehicleId { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string FilePath { get; set; }
        [Required]
        [Column(TypeName = "varchar(200)")]
        public string FileName { get; set; }
        public int? FileType { get; set; }
        [Column("CFGId")]
        public int? Cfgid { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DailyDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateTime { get; set; }
    }
}
