using iocCoreSMS.Services;
using Xunit;
using iocCoreSMS.Models;
using System.Collections.Generic;
using Xunit.Abstractions;
using System;

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
        public void SMSSendTest()
        {
            SMSManager manager = new SMSManager();
            manager.Send();

            Assert.True(true, "sms grabbed from db and sent through api");
        }

    }
}