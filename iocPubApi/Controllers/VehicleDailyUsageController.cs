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
    public class VehicleDailyUsageController : Controller
    {
        private readonly IVehicleDailyUsageRepository _repository;

        public VehicleDailyUsageController(IVehicleDailyUsageRepository repository)
        {
            _repository = repository;
        }

        // GET /api/VehicleDailyUsage/GetByDateRange/{vehicleName}/{beginDate}/{endDate}
        [HttpGet("GetByDateRange/{vehicleName}/{beginDate}/{endDate}")]
        public IEnumerable<VehicleDailyUsage> GetByDateRange(string vehicleName,
            DateTime beginDate, DateTime endDate)
        {
            return _repository.GetByDateRange(vehicleName, beginDate, endDate);
        }

        // GET /api/VehicleDailyUsage/GetByFleet/{fleetName}/{beginDate}/{endDate}
        [HttpGet("GetByFleet/{fleetName}/{beginDate}/{endDate}")]
        public IEnumerable<VehicleDailyUsage> GetByFleet(string fleetName,
            DateTime beginDate, DateTime endDate)
        {
            return _repository.GetByFleet(fleetName, beginDate, endDate);
        }

        // GET /api/VehicleDailyUsage/GetDaysSummaryByFleet/{fleetName}/{beginDate}/{endDate}
        [HttpGet("GetDaysSummaryByFleet/{fleetName}/{beginDate}/{endDate}")]
        public IEnumerable<VehicleDailyUsage> GetDaysSummaryByFleet(string fleetName,
            DateTime beginDate, DateTime endDate)
        {
            return _repository.GetDaysSummaryByFleet(fleetName, beginDate, endDate);
        }

    }
}