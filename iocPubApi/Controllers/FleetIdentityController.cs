using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using iocPubApi.Models;
using iocPubApi.Repositories;

namespace iocPubApi.Controllers
{
    [Route("[controller]")]
    public class FleetIdentityController : Controller
    {
        private readonly IFleetIdentityRepository _repository;

        public FleetIdentityController(IFleetIdentityRepository repository)
        {
            _repository = repository;
        }

        // GET api/fleetidentity
        [HttpGet]
        public IEnumerable<FleetIdentity> GetAll()
        {
            return _repository.GetAllFleets();
        }

        // GET api/fleetidentity/loginname/{loginname}
        [HttpGet("LoginName/{loginName}")]
        public IEnumerable<FleetIdentity> GetAllByUser(string loginName)
        {
            return _repository.GetAllFleetsByUser(loginName);
        }                            
    }
}
