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
            return _repository.GetAllByFleetName(fleetName);
        }

    }
}
