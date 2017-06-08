using System.Collections.Generic;
using iocPubApi.Models;

namespace iocPubApi.Repositories
{
    public interface IFleetIdentityRepository
    {
        IEnumerable<FleetIdentity> GetAllFleets();
        IEnumerable<FleetIdentity> GetAllFleetsByUser(string userID);
    }
}