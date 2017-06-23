using System;
using System.Collections.Generic;
using iocPubApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace iocPubApi.Repositories
{
    public class VehicleIdentityRepository : IVehicleIdentityRepository, IDisposable
    {
        private readonly io_onlineContext db;

        public VehicleIdentityRepository(io_onlineContext context)
        {
            db = context;
        }

        public IEnumerable<VehicleIdentity> GetAll()
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
            var db = this.db;
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

        /*
         * IO Control Admin (UserType=4)
            * load everything
         * Manufacturor Admin (UserType=64)
            * load related fleets
         * Others include Consumers
            * nothing
         */
        public IEnumerable<VehicleIdentity> GetAllByUser(string loginName)
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
            var db = this.db;
            IEnumerable<VehicleIdentity> ids;

            int? userType = (from users in db.IoUsers
                            where users.LogName == loginName
                            select users.UserType).SingleOrDefault();
            
            // return all fleets and vehicles when user is ioc user or admin 
            if (userType == 4 || userType == 2) 
                ids = this.GetAll(); 
            // return all vehicles when user is manufacturer or consumer
            else if (userType == 8 || userType == 16 || userType == 32 || userType == 64)
                ids = (from users in db.IoUsers 
                            join vehicle in db.IoVehicle
                                on users.CompanyId equals vehicle.CompanyId
                            join fleet in db.IoFleet
                                on vehicle.FleetId equals fleet.FleetId
                            where users.LogName == loginName
                            orderby vehicle.BusNo ascending
                            select new VehicleIdentity 
                            { 
                                Vid = vehicle.VehicleId, 
                                Vname = vehicle.BusNo,
                                Fid = fleet.FleetId,
                                Fname = fleet.Name
                            }).Distinct();
            else
            // return nothing when user not found in ioc user list, or no user type is found
                ids = null;

            return ids;
        }

        #region IDisposable Support
        private bool disposedValue = false; 

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}