using System;
using System.Collections.Generic;
using iocPubApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace iocPubApi.Repositories
{
    public class VehicleIdentityRepository : IVehicleIdentityRepository
    {
        private readonly io_onlineContext _context;

        public VehicleIdentityRepository(io_onlineContext context)
        {
            _context = context;
        }

        IEnumerable<VehicleIdentity> IVehicleIdentityRepository.GetAll()
        {
            var db = _context;
            var ids = from vehicle in db.IoVehicle
                        join fleet in db.IoFleet
                            on vehicle.FleetId equals fleet.FleetId
                        select new VehicleIdentity 
                        { 
                            Vid = vehicle.VehicleId, 
                            Vname = vehicle.BusNo,
                            Fid = fleet.FleetId,
                            Fname = fleet.Name
                        };
            
            return ids;
        }
    }
}