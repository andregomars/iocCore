using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using iocPubApi.Models;
using iocPubApi.Repositories;

namespace iocPubApi.Controllers
{
    [Route("api/[controller]")]
    public class VehicleStatusController : Controller
    {
        private readonly IVehicleStatusRepository _repository;

        public VehicleStatusController(IVehicleStatusRepository repository)
        {
            _repository = repository;
        }

        // GET api/vehicleidentity
        [HttpGet("{vehicleName}")]
        public VehicleStatus Get(string vehicleName)
        {
            return _repository.Get(vehicleName);
        }

    }
}
