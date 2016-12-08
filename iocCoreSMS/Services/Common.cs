using System;
using System.Collections.Generic;
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

        ///<summary>
        /// when send to muitple receiver, the expected ATT SMS adderss format is ["tel:21212,tel:343434"]
        /// ,but when send to single receiver, the expected ATT SMS adderss format is "tel:21212", there is no square bracket embraced.
        /// while c# array with one node only would still have the [] embraced, so there needs to be an adapter to follow ATT REST API specification
        ///</summary>
        public static object OutboundSMSRequestAdapter(OutboundSMSRequestWrapper request)
        {
            if (request.outboundSMSRequest.address.Length == 1)
            {
                return new OutboundSMSSingleReceiverRequestWrapper {
                    outboundSMSRequest = new OutboundSMSSingleReceiverRequest {
                        address = request.outboundSMSRequest.address[0],
                        message = request.outboundSMSRequest.message
                    }
                };
            }
            
            return request;
        }

        public static void AttachTelPrefix(OutboundSMSRequestWrapper request)
        {
            List<string> addressFormated = new List<string>();
            foreach(string addr in request.outboundSMSRequest.address)
            {
                addressFormated.Add(addr.Trim().Length >= 10 ? "tel:" + addr : addr);
            }
            request.outboundSMSRequest.address = addressFormated.ToArray();
        }

        public static void DetachTelPrefix(InboundSmsMessageListWrapper sms)
        {
            foreach(var msg in sms.InboundSmsMessageList.InboundSmsMessage)
            {
                msg.SenderAddress = RemoveTel(msg.SenderAddress);
                msg.DestinationAddress = RemoveTel(msg.DestinationAddress);
            }
        }

        private static string RemoveTel(string telephone)
        {
            string sanitizedNumber = telephone;
            if (sanitizedNumber.StartsWith("tel:"))
            {
                sanitizedNumber = telephone.Substring(4);
            }
            return sanitizedNumber;
        }
    }
}