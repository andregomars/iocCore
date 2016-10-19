using System.Collections.Generic;

namespace iocCoreSMS.Models
{
    public class InboundSmsMessageList
    {
        public List<InboundSmsMessage> InboundSmsMessage { get; set; }
        public string NumberOfMessagesInThisBatch { get; set; }
        public string ResourceUrl { get; set; }
        public string TotalNumberOfPendingMessages { get; set; }
    }

    public class InboundSmsMessageListWrapper
    {
        public InboundSmsMessageList InboundSmsMessageList { get; set; } 

        public override string ToString()
        {
            return $"Inbound SMS has {InboundSmsMessageList.NumberOfMessagesInThisBatch} messages in this batch";
        }
    }
}