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
    public class VehicleSnapshotHistoryController : Controller
    {
        private readonly IVehicleSnapshotHistoryRepository _repository;

        public VehicleSnapshotHistoryController(IVehicleSnapshotHistoryRepository repository)
        {
            _repository = repository;
        }

        // GET /api/VehicleSnapshotHistory/GetByVehicleName/{vehicleName}
        [HttpGet("GetByVehicleName/{vehicleName}")]
        public IEnumerable<VehicleSnapshot> GetByVehicleName(string vehicleName)
        {
            return _repository.GetByVehicleName(vehicleName);
        }
        
        // GET /api/VehicleSnapshotHistory/GetWholeDayByVehicleName/{vehicleName}/{date}
        [HttpGet("GetWholeDayByVehicleName/{vehicleName}/{date}")]
        public IEnumerable<VehicleSnapshot> GetWholeDayByVehicleName(string vehicleName, DateTime date)
        {
            return _repository.GetWholeDayByVehicleName(vehicleName, date);
        }
   }
}
