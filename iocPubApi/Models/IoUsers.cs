using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iocPubApi.Models
{
    [Table("IO_Users")]
    public partial class IoUsers
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string LogName { get; set; }
        public int? Sex { get; set; }
        [Column(TypeName = "varchar(120)")]
        public string Pwd { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Tel { get; set; }
        [Column(TypeName = "varchar(60)")]
        public string Email { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Mobile { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ValidDate { get; set; }
        public int? UserType { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string HeadImage { get; set; }
        public int? IsActive { get; set; }
        public int? Status { get; set; }
    }
}
