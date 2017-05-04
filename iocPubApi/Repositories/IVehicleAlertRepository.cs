using System.Collections.Generic;
using iocPubApi.Models;

namespace iocPubApi.Repositories
{
    public interface IVehicleAlertRepository
    {
        VehicleAlert GetByVehicleName(string vname);
        IEnumerable<VehicleAlert> GetAllByFleetName(string fname);
        IEnumerable<VehicleAlert> GetRecentAllByVehicleName(string vname);
    }
}