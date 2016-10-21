using System;
using iocCoreSMS.Models;

namespace iocCoreSMS.Services
{
    public static class Common
    {
        public static string[] SplitReceiverCodes(string receiverCodes)
        {
            if (String.IsNullOrEmpty(receiverCodes)) return null;

            string[] addrArray = null;
            try
            {
                addrArray = receiverCodes.Split(',');
            }
            catch(Exception e)
            {
                throw new Exception(@"Multiple receiver codes should be separated by comma, 
                    e.g ""tel:+16261112222,tel:+19092223333"", but the input is: " +
                    receiverCodes, e);
            }

            return addrArray;
        }

        public static object OutboundSMSRequestAdapter(OutboundSMSRequestWrapper request)
        {
            if (request.outboundSMSRequest.address.Length == 1)
            {
                return new OutboundSMSSingleReceiverRequestWrapper {
                    outboundSMSSingleReceiverRequest = new OutboundSMSSingleReceiverRequest {
                        address = request.outboundSMSRequest.address[0],
                        message = "test message"
                    }
                };
            }
            
            return request;
        }
    }
}