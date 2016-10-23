using System;
using iocCoreSMS.Models;

namespace iocCoreSMS.Services
{
    public interface ISMSManager
    {
        void Send();
        int Receive();
    }

    public sealed class SMSManager : ISMSManager
    {
        private static readonly SMSManager instance = new SMSManager();
        private string accessToken = null;

        public string AccessToken
        {
            get
            {
                if (String.IsNullOrEmpty(this.accessToken))
                {
                    RetrieveAccessToken();
                }

                return this.accessToken;
            }
        }
        
        #region properties
        public string UrlSendSMS { get; set; }

        public string UrlReceiveSMS { get; set; }

        public string UrlGetAccessToken { get; set; }

        public string AppScope { get; set; }

        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        public bool VerifyMessageDeliveryStatus { get; set; }

        public string RefreshToken { get; set; }
        public DateTime TokenExpiresDate { get; set; }

        #endregion properties

        static SMSManager()
        {
        }
        private SMSManager()
        {
        }

        public static SMSManager Instance
        {
            get
            {
                return instance;
            }
        }

        public void Send()
        {
            var msgBox = new MessageBox();
            var msgs = msgBox.GetMessages();
            

            //retrieve messages from db, send to sms api and get message ID
            foreach (var msg in msgs)
            {
                var reqWrapper = new OutboundSMSRequestWrapper {
                    outboundSMSRequest = new OutboundSMSRequest {
                        address = Common.SplitReceiverCodes(msg.ReceiverCode),
                        message = msg.Message
                    }
                };

                if (reqWrapper.outboundSMSRequest.address == null) continue;

                var res = new RestfulHelper()
                    .SendSMSAsync(UrlSendSMS, AccessToken, reqWrapper)
                    .GetAwaiter()
                    .GetResult();
                msg.MessageID = res.outboundSMSResponse.messageId;

                //if there is no need to check sms delivery status, then update status as "sent"
                if (!VerifyMessageDeliveryStatus)
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
                new RestfulHelper().ReceiveSMSAsync(UrlReceiveSMS, AccessToken).GetAwaiter().GetResult();
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

        public void RetrieveAccessToken()
        {
            AccessTokenResponse response = null;
            if (String.IsNullOrEmpty(this.accessToken))
            {
                response = new RestfulHelper()
                        .GetNewSMSClientToken(UrlGetAccessToken, AppKey, AppSecret, AppScope)
                        .GetAwaiter()
                        .GetResult();
                
            }
            else if(TokenExpiresDate <= DateTime.Now.AddHours(1))
            {
                response = new RestfulHelper()
                        .RefreshSMSClientToken(UrlGetAccessToken, AppKey, AppSecret, RefreshToken)
                        .GetAwaiter()
                        .GetResult();
            }

            if (response != null && !String.IsNullOrEmpty(response.access_token))
            {
                this.accessToken = response.token_type + " " + response.access_token;
                RefreshToken = response.refresh_token;
                TokenExpiresDate = DateTime.Now.AddSeconds(response.expires_in);
            }
        }
    }
}