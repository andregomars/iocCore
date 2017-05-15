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
            /* equivalent T-SQL
            select
                Vid = vehicle.VehicleId, 
                Vname = vehicle.BusNo,
                Fid = fleet.FleetId,
                Fname = fleet.Name
            from IO_Vehicle vehicle
            inner join IO_Fleet fleet
                on vehicle.FleetId = fleet.FleetID
             */
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

        IEnumerable<VehicleIdentity> IVehicleIdentityRepository.GetAllByUser(string loginName)
        {
            /* equivalent T-SQL
            select distinct
                --LogName = users.LogName,
                Vid = vehicle.VehicleId, 
                Vname = vehicle.BusNo,
                Fid = fleet.FleetId,
                Fname = fleet.Name
            from IO_Users users
            inner join IO_Vehicle vehicle
                on users.CompanyId = vehicle.CompanyId
            inner join IO_Fleet fleet
                on vehicle.FleetId = fleet.FleetID
            where users.LogName = 'lbt'
            --group by vehicle.VehicleId, vehicle.BusNo,
            -- fleet.FleetID,fleet.Name
             */
            var db = _context;
            var ids = (from users in db.IoUsers 
                        join vehicle in db.IoVehicle
                            on users.CompanyId equals vehicle.CompanyId
                        join fleet in db.IoFleet
                            on vehicle.FleetId equals fleet.FleetId
                        where users.LogName == loginName
                        select new VehicleIdentity 
                        { 
                            Vid = vehicle.VehicleId, 
                            Vname = vehicle.BusNo,
                            Fid = fleet.FleetId,
                            Fname = fleet.Name
                        }).Distinct();
            
            return ids;
        }
    }
}