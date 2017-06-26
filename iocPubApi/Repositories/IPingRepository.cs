using System.Collections.Generic;
using iocPubApi.Models;

namespace iocPubApi.Repositories
{
    public interface IPingRepository
    {
        string PingDB();
        string Reproduce();
    }
}