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
    public class FleetIdentityController : Controller
    {
        private readonly IFleetIdentityRepository _repository;
        private readonly IMemoryCache _cache;
        private const string Key_FleetIdentityList = "FleetIdentity_";

        public FleetIdentityController(IFleetIdentityRepository repository,
            IMemoryCache memCache)
        {
            _repository = repository;
            _cache = memCache;
        }

        // GET api/fleetidentity
        [HttpGet]
        public IEnumerable<FleetIdentity> GetAll()
        {
            return _repository.GetAllFleets();
        }

        // GET api/fleetidentity/getfleetbyname/{fleetname}
        [HttpGet("FleetName/{fleetName}")]
        public FleetIdentity GetFleetByName(string fleetName)
        {
            return _repository.GetFleetByName(fleetName);
        }

        // GET api/fleetidentity/loginname/{loginname}
        [HttpGet("LoginName/{loginName}")]
        public IEnumerable<FleetIdentity> GetAllByUser(string loginName)
        {
            IEnumerable<FleetIdentity> cacheEntry;
            string cacheKey = Key_FleetIdentityList + loginName;
            if(!_cache.TryGetValue(cacheKey, out cacheEntry))
            {
                cacheEntry = _repository.GetAllFleetsByUser(loginName);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10));

                _cache.Set(cacheKey, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }                            
    }
}
