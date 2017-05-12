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
    public class VehicleIdentityController : Controller
    {
        private readonly IVehicleIdentityRepository _repository;

        public VehicleIdentityController(IVehicleIdentityRepository repository)
        {
            _repository = repository;
        }

        // GET api/vehicleidentity
        [HttpGet]
        public IEnumerable<VehicleIdentity> GetAll()
        {
            return _repository.GetAll();
        }

    }
}
