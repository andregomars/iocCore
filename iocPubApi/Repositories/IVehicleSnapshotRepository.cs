using System.Collections.Generic;
using iocPubApi.Models;

namespace iocPubApi.Repositories
{
    public interface IVehicleSnapshotRepository
    {
        IEnumerable<VehicleSnapshot> GetByVehicleName(string vname);
    }
}