namespace iocCoreSMS.Models
{
    public class OutboundSMSResponse
    {
        public string messageId { get; set; }
        public ResourceReference resourceReference { get; set; }

    }
    
    public class OutboundSMSResponseWrapper
    {
        public OutboundSMSResponse outboundSMSResponse { get; set; }
        
        public override string ToString()
        {
            return $"Message id is {outboundSMSResponse.messageId}";
        }
    }
    
}