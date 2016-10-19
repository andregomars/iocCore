using System;
using System.Collections.Generic;
using iocCoreSMS.Models;

namespace iocCoreSMS.Services
{
    public class SMSManager
    {
        private string urlSendSMS = "https://api.att.com/sms/v3/messaging/outbox";
        private string urlReceiveSMS = "https://api.att.com/sms/v3/messaging/inbox/48507075";
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
        
        //return how many messages are received
        public int Receive()
        {
            InboundSmsMessageListWrapper wrapper = 
                new RestfulHelper().ReceiveSMSAsync(this.urlReceiveSMS).GetAwaiter().GetResult();
            MessageBox msgBox = new MessageBox();
            
            foreach (var sms in wrapper.InboundSmsMessageList.InboundSmsMessage)
            {
                SMSMessage msg = new SMSMessage();
                msg.ID = 0;
                msg.MessageID = sms.MessageId;
                msg.SubMessageID = null;
                msg.SMSType = "1";
                msg.SenderCode = sms.SenderAddress;
                msg.ReceiverCode = sms.DestinationAddress;
                msg.Status = "2";   //0: wait to send, 1: sent, 2: received
                msg.CreateTime = DateTime.Now;
                msg.SendTime = null;
                msg.Message = sms.Message;

                msgBox.PostMessage(msg);
            }

            int smsCount = 0;
            Int32.TryParse(wrapper.InboundSmsMessageList.NumberOfMessagesInThisBatch, out smsCount);
            return smsCount;
        }

        
    }
}