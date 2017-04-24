using iocPubApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace iocPubApi.Repositories
{
    public class IoFleetRepository: IIoFleetRepository
    {
        private readonly io_onlineContext _context;

        public IoFleetRepository(io_onlineContext context)
        {
            _context = context;
        }

        public IEnumerable<IoFleet> GetAll()
        {
            return _context.IoFleet.ToList();
        }
    }
}