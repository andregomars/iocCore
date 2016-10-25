using iocCoreSMS.Models;
using System.Collections.Generic;

namespace iocCoreSMS.Services
{
    public class MessageBox
    {
        private string baseUrl = "http://localhost:8005/api/SMS"; 

       public MessageBox()
        {}

        public List<SMSMessage> GetMessages()
        {
            string urlGetMessage = $"{baseUrl}?status=0";
            return new RestfulHelper().GetSMSMessageAsync(urlGetMessage).GetAwaiter().GetResult();
        }

        public bool UpdateMessage(SMSMessage msg)
        {
            string urlPutMessage = $"{baseUrl}/{msg.ID.ToString()}";
            return new RestfulHelper().UpdateSMSMessageAsync(urlPutMessage, msg).GetAwaiter().GetResult();
        }
        public SMSMessage PostMessage(SMSMessage msg)
        {
            string urlPostMessage = baseUrl;
            return new RestfulHelper().AddSMSMessageAsync(urlPostMessage, msg).GetAwaiter().GetResult();
        }
     
    }
}