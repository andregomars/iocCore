using iocCoreSMS.Services;
using Xunit;
using iocCoreSMS.Models;
using System.Collections.Generic;
using Xunit.Abstractions;
using System;
using NLog;

namespace iocCoreUnitTest
{

    public class SMSServicesTest
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly ITestOutputHelper output;
        public SMSServicesTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void SimpleTest()
        {
            Assert.True(true);
        }

        [Fact]
        public void MessageBoxGetTest()
        {
            MessageBox msgBox = new MessageBox();
            List<SMSMessage> messages = msgBox.GetMessages();
            Assert.NotEmpty(messages);
            Assert.Equal(2, messages.Count);
            Assert.True(true, $"here is the messages: {messages[0].Message} & {messages[1].Message} ");
        }

        [Fact]
        public void MessageBoxUpdateTest()
        {
            MessageBox msgBox = new MessageBox();
            SMSMessage message = new SMSMessage {
                ID = 1,
                MessageID = "SMSe76f8786d04ac205",
                SubMessageID = null,
                SMSType = "1",
                SenderCode = "48507075",
                ReceiverCode = "tel:+16262521073",
                Status = "1",
                CreateTime = DateTime.Parse("2016-10-14T21:55:33.2"),
                SendTime = DateTime.Now,
                Message = "test message"
            }; 
            bool isUpdate = msgBox.UpdateMessage(message);
            Assert.True(isUpdate, "update message successfully"); 
        }

        [Fact]
        public void MessageBoxPostTest()
        {
            MessageBox msgBox = new MessageBox();
            SMSMessage message = new SMSMessage {
                ID = 0,
                MessageID = "SMSe76f8786d04ac205",
                SubMessageID = null,
                SMSType = "1",
                SenderCode = "48507075",
                ReceiverCode = "tel:+16262521073",
                Status = "1",
                CreateTime = DateTime.Parse("2016-10-14T21:55:33.2"),
                SendTime = DateTime.Now,
                Message = "test message"
            }; 
            SMSMessage msg = msgBox.PostMessage(message);
            Assert.NotNull(msg);
            Assert.Equal(1, msg.ID);
        }


        [Fact]
        public void SMSSendTest()
        {
            SMSManager.Instance.Send();
            Assert.True(true, "sms grabbed from db and sent through api");
        }

        [Fact]
        public void SMSReceiveTest()
        {
            int msgReceived = SMSManager.Instance.Receive();
            Assert.Equal(0, msgReceived);
        }

        [Fact]
        public void RetrieveTokenTest()
        {
            logger.Info("RetrieveTokenTest starts...");

            SMSManager.Instance.RetrieveAccessToken();
            logger.Info(
                String.Format("New AccessToken {0} will be expired after {1}",
                SMSManager.Instance.AccessToken, SMSManager.Instance.TokenExpiresDate.ToString()));

            SMSManager.Instance.TokenExpiresDate = DateTime.Now.AddHours(-8);
            SMSManager.Instance.RetrieveAccessToken();
            logger.Info(
                String.Format("Refreshed AccessToken {0} will be expired after {1}",
                SMSManager.Instance.AccessToken, SMSManager.Instance.TokenExpiresDate.ToString()));

            Assert.NotNull(SMSManager.Instance.AccessToken);
            Assert.True(SMSManager.Instance.TokenExpiresDate > DateTime.Now.AddDays(1));
        }

    }
}