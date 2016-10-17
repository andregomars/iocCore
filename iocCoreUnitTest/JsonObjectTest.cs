using Xunit;
using iocCoreSMS.Models;
using Xunit.Abstractions;
using Newtonsoft.Json;

namespace iocCoreUnitTest
{
    public class JsonObjectTest
    {
        private readonly ITestOutputHelper output;
        public JsonObjectTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void PassingTest()
        {
            var request = new OutboundSMSRequest {
                address = "8002521111",
                message = "sms message test"
            }; 
            var wrapper = new OutboundSMSRequestWrapper{
                outboundSMSRequest = request
            };
            output.WriteLine("gogogo");
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
