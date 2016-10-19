using System.Collections.Generic;

namespace iocCoreSMS.Models
{
    public class DeliveryInfoList
    {
        public List<DeliveryInfo> DeliveryInfo { get; set; }
        public string ResourceUrl { get; set; }
    }
    
    public class DeliveryInfoListWrapper
    {
        public DeliveryInfoList DeliveryInfoList { get; set; }
        public override string ToString()
        {
            return $"Delivery status is {DeliveryInfoList.DeliveryInfo[0].DeliveryStatus}";
        }
    }
}