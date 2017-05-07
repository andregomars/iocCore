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
    public class VehicleSnapshotController : Controller
    {
        private readonly IVehicleSnapshotRepository _repository;

        public VehicleSnapshotController(IVehicleSnapshotRepository repository)
        {
            _repository = repository;
        }

        // GET /api/VehicleSnapshot/GetByVehicleName/{vehicleName}
        [HttpGet("GetByVehicleName/{vehicleName}")]
        public IEnumerable<VehicleSnapshot> GetByVehicleName(string vehicleName)
        {
            return _repository.GetByVehicleName(vehicleName);
        }
   }
}
