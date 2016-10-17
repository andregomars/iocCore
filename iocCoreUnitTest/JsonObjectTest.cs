using Xunit;
using iocCoreSMS.Models;

namespace iocCoreUnitTest
{
    public class JsonObjectTest
    {
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
            Assert.NotNull(wrapper);   
        }
    }
}
