using System;
using System.Collections.Generic;
using iocCoreSMS.Models;

namespace iocCoreSMS.Services
{
    public class SMSManager
    {
        private string urlSendSMS = "https://api.att.com/sms/v3/messaging/outbox";
        private bool verifyMessageDeliveryStatus = false;

        public SMSManager()
        {}

        public void Send()
        {
            var msgBox = new MessageBox();
            var msgs = msgBox.GetMessages();

            //retrieve messages from db, send to sms api and get message ID
            foreach (var msg in msgs)
            {
                var reqWrapper = new OutboundSMSRequestWrapper {
                    outboundSMSRequest = new OutboundSMSRequest {
                        address = msg.ReceiverCode,
                        message = msg.Message
                    }
                };

                var res = new RestfulHelper().SendSMSAsync(this.urlSendSMS, reqWrapper).GetAwaiter().GetResult();
                msg.MessageID = res.outboundSMSResponse.messageId;

                //if there is no need to check sms delivery status, then update status as "sent"
                if (!verifyMessageDeliveryStatus)
                {
                    msg.Status = "1";
                    msg.SendTime = DateTime.Now;
                }
            }

            //update message status back to DB 
            foreach (var msg in msgs)
            {
                if(!String.IsNullOrEmpty(msg.MessageID))
                {
                    msgBox.UpdateMessage(msg);
                }
            }


        }

        
    }
}