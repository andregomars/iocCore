using System.Collections.Generic;
using iocPubApi.Models;

namespace iocPubApi.Repositories
{
    public interface IHamsNetDataRepository
    {
        IEnumerable<HamsNetData> GetAll();
    }
}