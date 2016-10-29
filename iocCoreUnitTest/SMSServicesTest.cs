using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using iocCoreSMS.Models;
using iocCoreSMS.Services;

namespace iocCoreUnitTest
{

    public class SMSServicesTest
    {
        private ILogger logger;
        private ISMSConfiguration config;

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
            //LogConfig();
        }

        [Fact]
        public void SimpleTest()
        {
            logger.LogInformation("write log info.");
        }

        private void InitConfig()
        {
            config = new SMSConfiguration();
            config.ShortCode = Configuration["SMS.AttApi:ShortCode"];
            config.UrlSendSMS = Configuration["SMS.AttApi:UrlSendSMS"]
                .Replace("{{shortcode}}", config.ShortCode);
            config.UrlGetSMSDeliveryStatus = Configuration["SMS.AttApi:UrlGetSMSDeliveryStatus"];
            config.UrlReceiveSMS = Configuration["SMS.AttApi:UrlReceiveSMS"]
                .Replace("{{shortcode}}", config.ShortCode);
            config.UrlGetAccessToken = Configuration["SMS.AttApi:UrlGetAccessToken"];
            config.AppScope = Configuration["SMS.AttApi:AppScope"];
            config.AppKey = Configuration["SMS.AttApi:AppKey"];
            config.AppSecret = Configuration["SMS.AttApi:AppSecret"];
            config.VerifyMessageDeliveryStatus = 
                Convert.ToBoolean(Configuration["SMS.AttApi:VerifyMessageDeliveryStatus"]);
            config.DeliverySuccessCode = Configuration["SMS.AttApi:DeliverySuccessCode"];
            config.DeliveryFailureCode = Configuration["SMS.AttApi:DeliveryFailureCode"];
            config.BaseUrlMessageApi = Configuration["SMS.AttApi:BaseUrlMessageApi"];
        }

        private void LogConfig()
        {
            var type = config.GetType();
            PropertyInfo[] properties = type.GetProperties();

            logger.LogDebug("Start logging SMSConfig properties: ");
            foreach (PropertyInfo p in properties)
            {
                logger.LogDebug($"{p.Name}: {p.GetValue(config, null)}" );
            }
            logger.LogDebug("End logging SMSConfig properties");
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
            IMessageBox msgBox = new MessageBox(config);
            List<SMSMessage> messages = msgBox.GetMessages("0");
        }

        [Fact]
        public void MessageBoxUpdateTest()
        {
            IMessageBox msgBox = new MessageBox(config);
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
            IMessageBox msgBox = new MessageBox(config);
            SMSMessage message = new SMSMessage {
                ID = 0,
                MessageID = null,
                SubMessageID = null,
                SMSType = "1",
                SenderCode = "48507075",
                //ReceiverCode = "tel:+16262521073,tel:+16262170884",
                ReceiverCode = "tel:+16262521073",
                Status = "0",
                CreateTime = DateTime.Now,
                SendTime = null,
                Message = "posted message @ " + 
                    DateTime.Now.ToString("MM/dd/yyyy HH:mm")
            }; 
            SMSMessage msg = msgBox.PostMessage(message);
            Assert.NotNull(msg);
            //Assert.Equal(1, msg.ID);
        }


       [Fact]
        public void SMSSendTest()
        {
            try
            {
                IMessageBox msgBox = new MessageBox(config);
                ISMSManager manager = new SMSManager(config, msgBox);
                int sentCount = manager.Send();
                
                logger.LogInformation($"SMSSendTest sent out {sentCount} message(s)");
            }
            catch(Exception e)
            {
                logger.LogError(e.Message + "\r\n" + e.StackTrace);
                throw e;
            }
        }

        [Fact]
        public void SMSDeiveryStatusTest()
        {
            try
            {
                IMessageBox msgBox = new MessageBox(config);
                ISMSManager manager = new SMSManager(config, msgBox);
                int updatedCount = manager.GetSendStatus();

                
                logger.LogInformation($"SMSDeiveryStatusTest updated {updatedCount} message(s)");
            }
            catch(Exception e)
            {
                logger.LogError(e.Message + "\r\n" + e.StackTrace);
                throw e;
            }

        }

        [Fact]
        public void SMSReceiveTest()
        {
            IMessageBox msgBox = new MessageBox(config);
            ISMSManager manager = new SMSManager(config, msgBox);
            int msgReceived = manager.Receive();

            logger.LogInformation($"{msgReceived} text message(s) received");
        }

       [Fact]
        public void RetrieveTokenTest()
        {
            logger.LogInformation("RetrieveTokenTest starts...");

            IMessageBox msgBox = new MessageBox(config);
            var smsManager = new SMSManager(config, msgBox);
            smsManager.RetrieveAccessToken();

            logger.LogInformation(
                String.Format("New AccessToken {0} will be expired after {1}",
                smsManager.AccessToken, config.TokenExpiresDate.ToString()));

            config.TokenExpiresDate = DateTime.Now.AddHours(-8);
            smsManager.RetrieveAccessToken();
            logger.LogInformation(
                String.Format("Refreshed AccessToken {0} will be expired after {1}",
                smsManager.AccessToken, config.TokenExpiresDate.ToString()));

            Assert.NotNull(smsManager.AccessToken);
            Assert.True(config.TokenExpiresDate > DateTime.Now.AddDays(1));

            logger.LogInformation("RetrieveTokenTest ends...");
        }

    }
}