using System;
using System.Collections.Generic;
using iocPubApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Data;
using Dapper;

namespace iocPubApi.Repositories
{
    public class VehicleIdentityRepository : IVehicleIdentityRepository
    {
        private readonly io_onlineContext db;

        public VehicleIdentityRepository(io_onlineContext context)
        {
            db = context;
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
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
         * Admin (UserType=2, 4)
            * load everything
         * Manufacturor or Consumer (UserType=8, 16, 32, 64)
            * load related fleets
         * Others include Consumers
            * nothing
         */
        public IEnumerable<VehicleIdentity> GetAllByUser(string loginName)
        {
            IEnumerable<VehicleIdentity> vehicleList = null; 

            using(var conn = db.Database.GetDbConnection())
            {
                vehicleList = conn.Query<VehicleIdentity>("dbo.UP_HAMS_GetVehicleListByLoginName", 
                    new { LoginName = loginName },
                    commandType: CommandType.StoredProcedure);
            }
            return vehicleList;
        }

    }
}