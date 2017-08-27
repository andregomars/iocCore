using iocPubApi.Repositories;

namespace iocPubApiUnitTest
{
    public class PingRepository: IPingRepository
    {
        public string PingDB()
        {
            return "Pong";
        }

        public string Reproduce()
        {
            return "json created";
        }
    }
}