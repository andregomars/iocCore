using Xunit;
using iocCoreSMS.Models;
using Xunit.Abstractions;

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
    }
}
