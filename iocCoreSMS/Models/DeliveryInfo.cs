namespace iocCoreSMS.Models
{
    public class DeliveryInfo
    {
        public string Id { get; set; }
        public string Address { get; set; } 
        
        ///<summary>
        ///DeliveredToNetwork : The message is delivered to the network, but not yet delivered to the user's mobile device.
        ///DeliveredToTerminal : The message is successfully delivered to the user's mobile device.
        ///DeliveryImpossible : The message is not successful delivered, such as when the message is not delivered before it expires.
        ///</summary>
        public string DeliveryStatus { get; set; } 
        
    }
}