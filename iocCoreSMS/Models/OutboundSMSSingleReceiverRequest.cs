namespace iocCoreSMS.Models
{
    public class OutboundSMSSingleReceiverRequest
    {
        public string address { get; set; }
        public string message { get; set; }
    }

    public class OutboundSMSSingleReceiverRequestWrapper
    {
        public OutboundSMSSingleReceiverRequest outboundSMSRequest { get; set; }
    }
}