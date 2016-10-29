using System;
using iocCoreSMS.Models;

namespace iocCoreSMS.Services
{
    public interface ISMSManager
    {
        int Send();
        int GetSendStatus();
        int Receive();
        void RetrieveAccessToken();
    }

    ///<summary>
    /// SMS action status codes: 
    /// 0: wait to send 
    /// 1: sent 
    /// 2: sent and delivered
    /// 3: sent but deliver failed 
    /// 4: received
    /// 
    ///</summary>
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

        public int Send()
        {
            //get messages that haven't sent yet, where status = 0
            var msgs = m_msgBox.GetMessages("0");

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
                
                // if no qualified object gets from response, ignore this send request 
                if (res == null || res.outboundSMSResponse == null) continue;

                msg.MessageID = res.outboundSMSResponse.messageId;

                //if there is no need to check sms delivery status, update status as "sent and delivered" if not check
                if (!m_config.VerifyMessageDeliveryStatus)
                {
                    msg.Status = "2";   //sent and delivered
                    msg.SendTime = DateTime.Now;
                }
                else
                {
                    msg.Status = "1";   //just mark as sent
                    msg.SendTime = DateTime.Now;
                }
            }

            int sentCount = 0;
            //update message ID and status back to DB 
            foreach (var msg in msgs)
            {
                if(!String.IsNullOrEmpty(msg.MessageID))
                {
                    m_msgBox.UpdateMessage(msg);
                    sentCount++;
                }
            }

            return sentCount;
        }

        public int GetSendStatus()
        {
            //exit if ther is no need to verify delivery status
            if (!m_config.VerifyMessageDeliveryStatus) 
                return 0;

            //get messages already sent but not delivered yet, where status = 1
            var msgs = m_msgBox.GetMessages("1");

            //retrieve messages from db, send to sms api and get message ID
            foreach (var msg in msgs)
            {
                
                if ( String.IsNullOrEmpty(msg.MessageID) )  continue;

                string messageID = msg.MessageID.Trim();
                string urlGetStatus = $"{m_config.UrlGetSMSDeliveryStatus}/{messageID}";
                var res = new RestfulHelper()
                    .GetSMSSendStatusAsync(urlGetStatus, AccessToken)
                    .GetAwaiter()
                    .GetResult();

                bool isAllDelivered = true;
                bool isAnyDeliverFailed = false;

                // if no qualified delivery object got from response, leave message status not to update
                if (res == null || res.DeliveryInfoList == null || 
                    res.DeliveryInfoList.DeliveryInfo == null || 
                    res.DeliveryInfoList.DeliveryInfo.Count == 0)
                {
                    continue;
                }

                foreach (var deliverInfo in res.DeliveryInfoList.DeliveryInfo)
                {
                    // if any of the multiple receivers cannot be delivered to, set whole request deliver failed,
                    // and it won't be pulled out any more.
                    if ( deliverInfo.DeliveryStatus == m_config.DeliveryFailureCode )
                    {
                        isAnyDeliverFailed = true;
                        isAllDelivered = false;
                        break;
                    } 
                    else if ( deliverInfo.DeliveryStatus == m_config.DeliverySuccessCode )
                    {
                        continue;
                    }
                    // if any delivery status cannot be determined, set whole request not delivered yet,
                    // but the request would be pulled out the next round to check delivery status again. 
                    else
                    {
                        isAllDelivered = false;
                    }
                }

                if (isAnyDeliverFailed)
                {
                    msg.Status = "3";   //sent but deliver failed
                }
                else if (isAllDelivered)
                {
                    msg.Status = "2";   //sent and delivered
                }
            }

            int updatedCount = 0;
            //update message ID and status back to DB 
            foreach (var msg in msgs)
            {
                if(msg.Status == "2" || msg.Status == "3")
                {
                    m_msgBox.UpdateMessage(msg);
                    updatedCount++;
                }
            }

            return updatedCount;
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
                msg.Status = "4";   //recieved
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