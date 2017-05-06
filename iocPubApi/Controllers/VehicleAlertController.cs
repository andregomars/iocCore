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
    public class VehicleAlertController : Controller
    {
        private readonly IVehicleAlertRepository _repository;

        public VehicleAlertController(IVehicleAlertRepository repository)
        {
            _repository = repository;
        }

        // GET /api/VehicleAlert/GetRecentAllByVehicleName/{vehicleName}
        [HttpGet("GetRecentAllByVehicleName/{vehicleName}")]
        public IEnumerable<VehicleAlert> GetRecentAllByVehicleName(string vehicleName)
        {
            return _repository.GetRecentAllByVehicleName(vehicleName);
        }
    }
}
