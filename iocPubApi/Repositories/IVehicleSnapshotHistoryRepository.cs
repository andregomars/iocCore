using System;
using System.Collections.Generic;
using iocPubApi.Models;

namespace iocPubApi.Repositories
{
    public interface IVehicleSnapshotHistoryRepository
    {
        IEnumerable<VehicleSnapshot> GetByVehicleName(string vname);
        IEnumerable<VehicleSnapshot> GetWholeDayByVehicleName(string vname, DateTime date);
    }
}