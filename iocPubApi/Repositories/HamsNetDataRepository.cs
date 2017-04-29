using iocPubApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace iocPubApi.Repositories
{
    public class HamsNetDataRepository: IHamsNetDataRepository
    {
        private readonly io_onlineContext _context;

        public HamsNetDataRepository(io_onlineContext context)
        {
            _context = context;
        }

        public IEnumerable<HamsNetData> GetAll()
        {
            return _context.HamsNetData.ToList();
        }
    }
}