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
    public class VehicleStatusController : Controller
    {
        private readonly IVehicleStatusRepository _repository;
        private readonly IMemoryCache _cache;
        private const string Key_FleetStatusList = "FleetStatusList_";

        public VehicleStatusController(IVehicleStatusRepository repository,
            IMemoryCache memCache)
        {
            _repository = repository;
            _cache = memCache; 
        }

        // GET /api/VehicleStatus/GetByVehicleName/{vehicleName}
        [HttpGet("GetByVehicleName/{vehicleName}")]
        public VehicleStatus GetByVehicleName(string vehicleName)
        {
            return _repository.GetByVehicleName(vehicleName);
        }

        // GET /api/VehicleStatus/GetAllByFleetName/{fleetName}
        [HttpGet("GetAllByFleetName/{fleetName}")]
        public IEnumerable<VehicleStatus> GetAllByFleetName(string fleetName)
        {
            IEnumerable<VehicleStatus> cacheEntry;
            string cacheKey = Key_FleetStatusList + fleetName;
            if(!_cache.TryGetValue(Key_FleetStatusList, out cacheEntry))
            {
                cacheEntry = _repository.GetAllByFleetName(fleetName);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }

        // GET /api/VehicleStatus/GetRecentAllByVehicleName/{vehicleName}
        [HttpGet("GetRecentAllByVehicleName/{vehicleName}")]
        public IEnumerable<VehicleStatus> GetRecentAllByVehicleName(string vehicleName)
        {
           return _repository.GetRecentAllByVehicleName(vehicleName);
        }

        // GET /api/VehicleStatus/GetWholeDayByVehicleName/{vehicleName}
        [HttpGet("GetWholeDayByVehicleName/{vehicleName}")]
        public IEnumerable<VehicleStatus> GetWholeDayByVehicleName(string vehicleName, DateTime date)
        {
            return _repository.GetWholeDayByVehicleName(vehicleName, date);
        }
    }
}
