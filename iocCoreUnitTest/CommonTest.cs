using Xunit;
using iocCoreSMS.Models;
using iocCoreSMS.Services;
using System.Collections.Generic;

namespace iocCoreUnitTest
{
    public class CommonTest
    {
        [Fact]
        public void AttachTelPrefixTest()
        {
            OutboundSMSRequestWrapper wrapper = new OutboundSMSRequestWrapper();
            wrapper.outboundSMSRequest = new OutboundSMSRequest();
            wrapper.outboundSMSRequest.address = new string[]{"6262521073","48507075","6262170884"};
            Common.AttachTelPrefix(wrapper);

            string[] expected = new string[]{"tel:6262521073","48507075","tel:6262170884"};
            Assert.Equal(expected, wrapper.outboundSMSRequest.address);
        }

        [Fact]
        public void DetachTelPrefixTest()
        {
            InboundSmsMessageListWrapper wrapper = new InboundSmsMessageListWrapper();
            wrapper.InboundSmsMessageList = new InboundSmsMessageList();
            wrapper.InboundSmsMessageList.InboundSmsMessage = new List<InboundSmsMessage>();
            InboundSmsMessage inMsg = new InboundSmsMessage();
            inMsg.SenderAddress = "48507075";
            inMsg.DestinationAddress = "tel:6262521073";
            InboundSmsMessage inMsg2 = new InboundSmsMessage();
            inMsg2.SenderAddress = "tel:6262170884";
            inMsg2.DestinationAddress = "48507075";

            wrapper.InboundSmsMessageList.InboundSmsMessage.Add(inMsg);
            wrapper.InboundSmsMessageList.InboundSmsMessage.Add(inMsg2);
            
            Common.DetachTelPrefix(wrapper);

            
            Assert.Equal(wrapper.InboundSmsMessageList.InboundSmsMessage[0].SenderAddress, "48507075");
            Assert.Equal(wrapper.InboundSmsMessageList.InboundSmsMessage[0].DestinationAddress, "6262521073");
            Assert.Equal(wrapper.InboundSmsMessageList.InboundSmsMessage[1].SenderAddress, "6262170884");
            Assert.Equal(wrapper.InboundSmsMessageList.InboundSmsMessage[1].DestinationAddress, "48507075");
        }
    }
}