namespace iocCoreSMS.Models
{
    public class InboundSmsMessage
    {
        public string DestinationAddress { get; set; }
        public string MessageId { get; set; }
        public string Message { get; set; }
        public string SenderAddress { get; set; }

    }
}