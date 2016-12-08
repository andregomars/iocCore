using System;

namespace iocCoreSMS.Models
{
    public class SMSMessage
    {
       public Guid ID { get; set; } 
       public string MessageID { get; set; }
       public string SubMessageID { get; set; }
       public string SMSType { get; set; }
       public string SenderCode { get; set; }
       public string ReceiverCode { get; set; }
       public string Status { get; set; }
       public DateTime? CreateTime { get; set; }
       public DateTime? SendTime { get; set; }
       public string Message { get; set; }
       public short? IsDone {get; set;}
    }
}