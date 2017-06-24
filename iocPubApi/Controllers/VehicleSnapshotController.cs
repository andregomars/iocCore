using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using iocPubApi.Models;
using iocPubApi.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace iocPubApi.Controllers
{
    [Route("[controller]")]
    public class VehicleSnapshotController : Controller
    {
        private readonly IVehicleSnapshotRepository _repository;
        private readonly IMemoryCache _cache;
        private const string Key_VehicleSnapshot = "VehicleSnapshot_";

        public VehicleSnapshotController(IVehicleSnapshotRepository repository,
            IMemoryCache memCache)
        {
            _repository = repository;
            _cache = memCache;
        }

        // GET /api/VehicleSnapshot/GetByVehicleName/{vehicleName}
        [HttpGet("GetByVehicleName/{vehicleName}")]
        public IEnumerable<VehicleSnapshot> GetByVehicleName(string vehicleName)
        {
            IEnumerable<VehicleSnapshot> cacheEntry;
            string cacheKey = Key_VehicleSnapshot + vehicleName;
            if(!_cache.TryGetValue(cacheKey, out cacheEntry))
            {
                cacheEntry = _repository.GetByVehicleName(vehicleName);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }
        
        // GET /api/VehicleSnapshot/GetWholeDayByVehicleName/{vehicleName}/{date}
        [HttpGet("GetWholeDayByVehicleName/{vehicleName}/{date}")]
        public IEnumerable<VehicleSnapshot> GetWholeDayByVehicleName(string vehicleName, DateTime date)
        {
            IEnumerable<VehicleSnapshot> cacheEntry;
            string cacheKey = Key_VehicleSnapshot + vehicleName + "_" + date.ToString("yyMMdd");
            if(!_cache.TryGetValue(cacheKey, out cacheEntry))
            {
                cacheEntry = _repository.GetWholeDayByVehicleName(vehicleName, date);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }
   }
}
