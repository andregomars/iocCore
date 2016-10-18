using iocCoreSMS.Services;
using Xunit;
using iocCoreSMS.Models;
using System.Collections.Generic;
using Xunit.Abstractions;

namespace iocCoreUnitTest
{
    public class SMSServicesTest
    {
        private readonly ITestOutputHelper output;
        public SMSServicesTest(ITestOutputHelper output)
        {
            this.output = output;
        }
        
        [Fact]
        public void MessageBoxTest()
        {
            MessageBox msgBox = new MessageBox();
            List<SMSMessage> messages = msgBox.GetMessages();
            Assert.NotEmpty(messages);
            Assert.Equal(2, messages.Count);
            Assert.True(true, $"here is the messages: {messages[0].Message} & {messages[1].Message} ");


        }

    }
}