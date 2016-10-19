using iocCoreSMS.Models;
using System.Collections.Generic;

namespace iocCoreSMS.Services
{
    public class MessageBox
    {
        private string urlGetMessage = "http://www.mocky.io/v2/580472bb240000db2a135c91?status=0";
        private string urlPutMessage = "http://www.mocky.io/v2/580472bb240000db2a135c91?status=0";

       public MessageBox()
        {}

        public List<SMSMessage> GetMessages()
        {
            return new RestfulHelper().GetSMSMessageAsync(this.urlGetMessage).GetAwaiter().GetResult();
        }

        public bool UpdateMessage(SMSMessage msg)
        {
            return new RestfulHelper().UpdateSMSMessageAsync(this.urlPutMessage, msg).GetAwaiter().GetResult();
        }

       
    }
}