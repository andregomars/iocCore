using System.Collections.Generic;
using iocPubApi.Models;

namespace iocPubApi.Repositories
{
    public interface IVehicleSnapshotRepository
    {
        VehicleSnapshot GetByVehicleName(string vname);
    }
}