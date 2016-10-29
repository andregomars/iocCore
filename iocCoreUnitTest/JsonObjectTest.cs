using Xunit;
using iocCoreSMS.Models;
using iocCoreSMS.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog.Extensions.Logging;
using System.IO;

namespace iocCoreUnitTest
{
    public class JsonObjectTest
    {
        private ILogger logger;

        public IConfigurationRoot Configuration { get; private set; }

        public JsonObjectTest()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            ILoggerFactory loggerFactory = new LoggerFactory()
                .AddNLog();
            logger = loggerFactory.CreateLogger<SMSServicesTest>();
        }
        
        [Fact]
        public void OutboundSMSSerializeTest()
        {
            string receivercodes = "tel:+18002521111";
            //string receivercodes = "tel:+18002521111,tel:+19793235555";
            ISMSConfiguration config = new SMSConfiguration();
            config.BaseUrlMessageApi = 
                Configuration["SMS.AttApi:BaseUrlMessageApi"];
            var msgBox = new MessageBox(config);
            var msgs = msgBox.GetMessages("0");
            //string receivercodes = msgs[0].ReceiverCode;

            var wrapper = new OutboundSMSRequestWrapper {
                    outboundSMSRequest = new OutboundSMSRequest {
                        address = Common.SplitReceiverCodes(receivercodes),
                        message = "test message"
                    }
                };
            
            object requestObj = Common.OutboundSMSRequestAdapter(wrapper);
            string output = JsonConvert.SerializeObject(requestObj);
            logger.LogInformation("output of OutboundSMSSerializeTest: ");
            logger.LogInformation(output);     
            Assert.NotNull(wrapper);   
        }

        [Fact]
        public void DeliveryListDeserializeTest()
        {
            string jsonText = @"{
                ""DeliveryInfoList"": {
                    ""DeliveryInfo"": [
                        {
                            ""Id"": ""msg0"",
                            ""Address"": ""tel:+16262170884"",
                            ""DeliveryStatus"": ""DeliveryImpossible""
                        },
                        {
                            ""Id"": ""msg1"",
                            ""Address"": ""tel:+16262521073"",
                            ""DeliveryStatus"": ""DeliveredToTerminal""
                        }
                    ],
                    ""ResourceUrl"": ""https://ewr2-api.att.com/sms/v3/messaging/outbox/SMS28d28a6afff7f149""
                }
            }";

            var deliveryList = JsonConvert.DeserializeObject<DeliveryInfoListWrapper>(jsonText);
            Assert.NotNull(deliveryList);
            Assert.Equal(2, deliveryList.DeliveryInfoList.DeliveryInfo.Count);
            Assert.Equal("msg0", deliveryList.DeliveryInfoList.DeliveryInfo[0].Id);
        }

       
        [Fact]
        public void SMSMessageTest()
        {
            string jsonText = @"{
        ""ID"": 1,
        ""MessageID"": null,
        ""SubMessageID"": null,
        ""SMSType"": ""1    "",
        ""SenderCode"": ""48507075"",
        ""RecieverCode"": ""tel:+16262521073"",
        ""Status"": ""0    "",
        ""CreateTime"": ""2016-10-14T21:55:33.2"",
        ""SendTime"": null,
        ""Message"": ""test message""
            }";

           var message = JsonConvert.DeserializeObject<SMSMessage>(jsonText);
           Assert.NotNull(message);
           Assert.Equal("48507075", message.SenderCode);
           Assert.Equal("test message", message.Message);

        }
    }
}
