using iocPubApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace iocPubApi.Repositories
{
    public class HamsNetDataItemRepository: IHamsNetDataItemRepository
    {
        private readonly io_onlineContext _context;

        public HamsNetDataItemRepository(io_onlineContext context)
        {
            _context = context;
        }

        public IEnumerable<HamsNetDataItem> GetAll()
        {
            return _context.HamsNetDataItem.ToList();
        }
    }
}