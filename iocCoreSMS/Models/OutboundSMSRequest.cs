namespace iocCoreSMS.Models
{
    public class OutboundSMSRequest
    {
        public string address { get; set; }
        public string message { get; set; }
    }

    public class OutboundSMSRequestWrapper
    {
        public OutboundSMSRequest outboundSMSRequest { get; set; }
    }
}