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

    }
}