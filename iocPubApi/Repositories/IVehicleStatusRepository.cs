using System.Collections.Generic;
using iocPubApi.Models;

namespace iocPubApi.Repositories
{
    public interface IVehicleStatusRepository
    {
        VehicleStatus GetByVehicleName(string vname);
        IEnumerable<VehicleStatus> GetAllByFleetName(string fname);
        IEnumerable<VehicleStatus> GetRecentAllByVehicleName(string vname);
    }
}