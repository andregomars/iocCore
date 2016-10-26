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
        ISMSConfiguration m_config;
        IMessageBox m_msgBox;

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
        
        public SMSManager(ISMSConfiguration config, IMessageBox msgBox) 
        {
            m_config = config;
            m_msgBox = msgBox;
        }

        public void Send()
        {
            var msgs = m_msgBox.GetMessages();

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
                    .SendSMSAsync(m_config.UrlSendSMS, AccessToken, reqWrapper)
                    .GetAwaiter()
                    .GetResult();
                msg.MessageID = res.outboundSMSResponse.messageId;

                //if there is no need to check sms delivery status, then update status as "sent"
                if (!m_config.VerifyMessageDeliveryStatus)
                {
                    msg.Status = "1";
                    msg.SendTime = DateTime.Now;
                }
            }

            //update message ID and status back to DB 
            foreach (var msg in msgs)
            {
                if(!String.IsNullOrEmpty(msg.MessageID))
                {
                    m_msgBox.UpdateMessage(msg);
                }
            }
        }
        
        //return how many messages are received
        public int Receive()
        {
            var wrapper = new RestfulHelper()
                .ReceiveSMSAsync(m_config.UrlReceiveSMS, AccessToken)
                .GetAwaiter()
                .GetResult();
            
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

                m_msgBox.PostMessage(msg);
            }

            int smsCount = 0;
            Int32.TryParse(wrapper.InboundSmsMessageList.NumberOfMessagesInThisBatch, out smsCount);
            return smsCount;
        }

        ///<summary>
        /// According to ATT Rest API specification, it needs to follow OAuth2.0 agreement to 
        /// perform access token retrieving dynamically before each API request. 
        ///</summary>
        public void RetrieveAccessToken()
        {
            AccessTokenResponse response = null;
            if (String.IsNullOrEmpty(this.accessToken))
            {
                response = new RestfulHelper()
                        .GetNewSMSClientToken(m_config.UrlGetAccessToken, m_config.AppKey, 
                            m_config.AppSecret, m_config.AppScope)
                        .GetAwaiter()
                        .GetResult();
                
            }
            else if(m_config.TokenExpiresDate <= DateTime.Now.AddHours(1))
            {
                response = new RestfulHelper()
                        .RefreshSMSClientToken(m_config.UrlGetAccessToken, m_config.AppKey, 
                            m_config.AppSecret, m_config.RefreshToken)
                        .GetAwaiter()
                        .GetResult();
            }

            //assign access token, refresh token and token expire date
            if (response != null && !String.IsNullOrEmpty(response.access_token))
            {
                this.accessToken = response.token_type + " " + response.access_token;
                m_config.RefreshToken = response.refresh_token;
                m_config.TokenExpiresDate = DateTime.Now.AddSeconds(response.expires_in);
            }
        }
    }
}