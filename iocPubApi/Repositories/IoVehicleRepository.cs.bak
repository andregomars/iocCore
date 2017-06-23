using System.Collections.Generic;
using System.Linq;

using iocPubApi.Models;

namespace iocPubApi.Repositories
{
    public class IoVehicleRepository: IIoVehicleRepository
    {
        private readonly io_onlineContext _context;

        public IoVehicleRepository(io_onlineContext context)
        {
            _context = context;
        }
        
        public IEnumerable<IoVehicle> GetAll()
        {
            return _context.IoVehicle.ToList();
        }
    }
}