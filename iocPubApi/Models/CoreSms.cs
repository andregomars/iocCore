using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iocPubApi.Models
{
    [Table("Core_SMS")]
    public partial class CoreSms
    {
        [Column("ID")]
        public Guid Id { get; set; }
        [Column("MessageID", TypeName = "varchar(50)")]
        public string MessageId { get; set; }
        [Column("SubMessageID", TypeName = "varchar(50)")]
        public string SubMessageId { get; set; }
        [Column("SMSType", TypeName = "char(5)")]
        public string Smstype { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string SenderCode { get; set; }
        [Column(TypeName = "varchar(2000)")]
        public string ReceiverCode { get; set; }
        [Column(TypeName = "char(5)")]
        public string Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SendTime { get; set; }
        public string Message { get; set; }
        public short? IsDone { get; set; }
    }
}
