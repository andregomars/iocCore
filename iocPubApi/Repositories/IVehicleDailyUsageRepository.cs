using System;
using System.Collections.Generic;
using iocPubApi.Models;

namespace iocPubApi.Repositories
{
    public interface IVehicleDailyUsageRepository
    {
        IEnumerable<VehicleDailyUsage> GetByDateRange(string vname, DateTime beginDate, DateTime endDate);
    }
}