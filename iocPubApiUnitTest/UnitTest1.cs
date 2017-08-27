using System;
using Xunit;
using iocPubApi.Controllers;

namespace iocPubApiUnitTest
{
    public class PingControllerShould
    {
        private readonly PingRepository _repository;

        public PingControllerShould()
        {
            _repository = new PingRepository();
        }

        [Fact]
        public void ReturnPongWhenPingDB()
        {
            var result = _repository.PingDB();
            Assert.Equal(result, "Pong");
        }
    }
}
