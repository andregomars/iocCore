using iocCoreSMS.Models;
using System.Collections.Generic;

namespace iocCoreSMS.Services
{
    public interface IMessageBox
    {
        List<SMSMessage> GetMessages(string status);
        bool UpdateMessage(SMSMessage msg);
        SMSMessage PostMessage(SMSMessage msg);
    }

    public class MessageBox : IMessageBox
    {
        private ISMSConfiguration m_config;

        public MessageBox(ISMSConfiguration config)
        {
            m_config = config;
        }

        public List<SMSMessage> GetMessages(string status)
        {
            string urlGetMessage = $"{m_config.BaseUrlMessageApi}?status={status}";
            return new RestfulHelper().GetSMSMessageAsync(urlGetMessage).GetAwaiter().GetResult();
        }

        public bool UpdateMessage(SMSMessage msg)
        {
            string urlPutMessage = $"{m_config.BaseUrlMessageApi}/{msg.ID.ToString()}";
            return new RestfulHelper().UpdateSMSMessageAsync(urlPutMessage, msg).GetAwaiter().GetResult();
        }
        public SMSMessage PostMessage(SMSMessage msg)
        {
            string urlPostMessage = m_config.BaseUrlMessageApi;
            return new RestfulHelper().AddSMSMessageAsync(urlPostMessage, msg).GetAwaiter().GetResult();
        }
     
    }
}