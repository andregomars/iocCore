using System.Collections.Generic;
using iocPubApi.Models;

namespace iocPubApi.Repositories
{
    public interface IIoFleetRepository
    {
        IEnumerable<IoFleet> GetAll();
    }
}