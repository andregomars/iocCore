using Xunit;
using iocCoreSMS.Models;
using iocCoreSMS.Services;
using Xunit.Abstractions;
using Newtonsoft.Json;
using NLog;

namespace iocCoreUnitTest
{
    public class JsonObjectTest
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly ITestOutputHelper output;
        public JsonObjectTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void OutboundSMSSerializeTest()
        {
            string receivercodes = "tel:+18002521111";
            //string receivercodes = "tel:+18002521111,tel:+19793235555";

            var msgBox = new MessageBox();
            var msgs = msgBox.GetMessages();
            //string receivercodes = msgs[0].ReceiverCode;

            var wrapper = new OutboundSMSRequestWrapper {
                    outboundSMSRequest = new OutboundSMSRequest {
                        address = Common.SplitReceiverCodes(receivercodes),
                        message = "test message"
                    }
                };
            
            object requestObj = Common.OutboundSMSRequestAdapter(wrapper);
            string output = JsonConvert.SerializeObject(requestObj);
            logger.Info(output);     
            Assert.NotNull(wrapper);   
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
