using System.Collections.Generic;
using iocPubApi.Models;

namespace iocPubApi.Repositories
{
    public interface IVehicleIdentityRepository
    {
        IEnumerable<VehicleIdentity> GetAll();
        IEnumerable<VehicleIdentity> GetAllByUser(string userID);

    }
}