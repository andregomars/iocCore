using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

using iocPubApi.Models;
using iocPubApi.Repositories;


namespace iocPubApi.Controllers
{
    [Route("[controller]")]
    public class VehicleIdentityController : Controller
    {
        private readonly IVehicleIdentityRepository _repository;
        private readonly IMemoryCache _cache;
        private const string Key_VehicleList = "VehicleList_";

        public VehicleIdentityController(IVehicleIdentityRepository repository,
            IMemoryCache memCache)
        {
            _repository = repository;
            _cache = memCache;
        }

        // GET api/vehicleidentity
        [HttpGet]
        public IEnumerable<VehicleIdentity> GetAll()
        {
            return _repository.GetAll();
        }

        // GET api/vehicleidentity/loginname/{loginname}
        [HttpGet("LoginName/{loginName}")]
        public IEnumerable<VehicleIdentity> GetAllByUser(string loginName)
        {
            IEnumerable<VehicleIdentity> cacheEntry;
            string cacheKey = Key_VehicleList + loginName;
            if(!_cache.TryGetValue(cacheKey, out cacheEntry))
            {
                cacheEntry = _repository.GetAllByUser(loginName);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10));

                _cache.Set(cacheKey, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }                            
    }
}
