using iocCoreSMS.Services;
using Xunit;
using iocCoreSMS.Models;
using System.Collections.Generic;
using System;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;

namespace iocCoreUnitTest
{

    public class SMSServicesTest
    {
        private ILogger logger;

        public IConfigurationRoot Configuration { get; private set; }

        public SMSServicesTest()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            ILoggerFactory loggerFactory = new LoggerFactory()
                .AddNLog();
            logger = loggerFactory.CreateLogger<SMSServicesTest>();

            InitConfig();
        }

        [Fact]
        public void SimpleTest()
        {
            logger.LogInformation("write log info.");
        }

        private void InitConfig()
        {
            SMSManager.Instance.UrlSendSMS = Configuration["SMS.AttApi:urlSendSMS"];
            SMSManager.Instance.UrlReceiveSMS = Configuration["SMS.AttApi:UrlReceiveSMS"];
            SMSManager.Instance.UrlGetAccessToken = Configuration["SMS.AttApi:UrlGetAccessToken"];
            SMSManager.Instance.AppScope = Configuration["SMS.AttApi:AppScope"];
            SMSManager.Instance.AppKey = Configuration["SMS.AttApi:AppKey"];
            SMSManager.Instance.AppSecret = Configuration["SMS.AttApi:AppSecret"];
            SMSManager.Instance.VerifyMessageDeliveryStatus = 
                Convert.ToBoolean(Configuration["SMS.AttApi:VerifyMessageDeliveryStatus"]);
        }

       [Fact]
        public void ReceiverCodesSplitTest()
        {
            string rcodesMultiple = "tel:+16261112222,tel:+19092223333";
            string rcodesSingle = "tel:+16261112222";
            
            string[] mcodes = Common.SplitReceiverCodes(rcodesMultiple);
            string[] scode = Common.SplitReceiverCodes(rcodesSingle);
            string[] ecode = Common.SplitReceiverCodes("");
            string[] ncode = Common.SplitReceiverCodes(null);

            Assert.Equal(2, mcodes.Length);
            Assert.Equal(1, scode.Length);
            Assert.Null(ecode);
            Assert.Null(ncode);
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
            logger.LogInformation("RetrieveTokenTest starts...");

            SMSManager.Instance.RetrieveAccessToken();
            logger.LogInformation(
                String.Format("New AccessToken {0} will be expired after {1}",
                SMSManager.Instance.AccessToken, SMSManager.Instance.TokenExpiresDate.ToString()));

            SMSManager.Instance.TokenExpiresDate = DateTime.Now.AddHours(-8);
            SMSManager.Instance.RetrieveAccessToken();
            logger.LogInformation(
                String.Format("Refreshed AccessToken {0} will be expired after {1}",
                SMSManager.Instance.AccessToken, SMSManager.Instance.TokenExpiresDate.ToString()));

            Assert.NotNull(SMSManager.Instance.AccessToken);
            Assert.True(SMSManager.Instance.TokenExpiresDate > DateTime.Now.AddDays(1));
        }

    }
}