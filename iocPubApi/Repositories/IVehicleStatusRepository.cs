using System.Collections.Generic;
using iocPubApi.Models;

namespace iocPubApi.Repositories
{
    public interface IVehicleStatusRepository
    {
        VehicleStatus Get(string vname);
    }
}