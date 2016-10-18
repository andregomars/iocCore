using iocCoreSMS.Models;
using System.Collections.Generic;

namespace iocCoreSMS.Services
{
    public class MessageBox
    {
        private string sourceUrl = "http://www.mocky.io/v2/58046a0b240000d829135c85?status=0";

        // public MessageBox(string sourceUrl)
        // {
        //     this.sourceUrl = sourceUrl;
        // }
        
        public MessageBox()
        {}

        public List<SMSMessage> GetMessages()
        {
            return RestfulHelper.GetSMSMessageAsync(sourceUrl).GetAwaiter().GetResult();
        }
    }
}