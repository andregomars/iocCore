using System;
using System.IO;
using System.Collections.Generic;
using iocPubApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Dapper;
using System.Data;

namespace iocPubApi.Repositories
{
    public class FleetIdentityRepository : IFleetIdentityRepository
    {
        private readonly io_onlineContext db;
        private string iconBaseUrl;
        public IConfigurationRoot Configuration { get; }

        public FleetIdentityRepository(io_onlineContext context)
        {
            db = context;
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            iconBaseUrl = Configuration["HAMS.Api:FleetIcon.BaseUrl"];
        }

        public IEnumerable<FleetIdentity> GetAllFleets() 
        {
            /* equivalent T-SQL
            select distinct
                Fname = fleet.Name,
                VehicleType = fleet.VehicleType,
                Icon = 'http://52.35.12.17/online2017/hams/images/fleeticon/'+ 
                    RTRIM(Icon) + '/' + RTRIM(VehicleType) + '.png'
            from IO_Vehicle vehicle
            inner join IO_Fleet fleet
                on vehicle.FleetId = fleet.FleetID
             */
            var db = this.db;
            var fleets = (from vehicle in db.IoVehicle
                        join fleet in db.IoFleet
                            on vehicle.FleetId equals fleet.FleetId
                        select new FleetIdentity 
                        { 
                            Fname = fleet.Name,
                            Remark = fleet.Remark.Trim(),
                            VehicleType = fleet.VehicleType.Trim(),
                            Icon = GetIconUrl(fleet.Icon, fleet.VehicleType)
                        }).GroupBy(r => new { r.Fname, r.Remark, r.VehicleType, r.Icon })
                            .Select(g => new FleetIdentity{
                                Fname = g.Key.Fname,
                                Remark = g.Key.Remark,
                                VehicleType = g.Key.VehicleType,
                                Icon = g.Key.Icon
                            });
            
            return fleets;
        }

        private string GetIconUrl(string icon, string vehicleType) {
            string defaultUrl = "http://52.35.12.17/online2017/hams/images/fleeticon/";
            string defaultIcon = "default";
            string defaultVehicleType = "bus";

            string baseUrl = String.IsNullOrEmpty(iconBaseUrl) ? defaultUrl : iconBaseUrl;
            string pathIcon = String.IsNullOrEmpty(icon) || String.IsNullOrWhiteSpace(icon) ? defaultIcon : icon.Trim();
            string pathVehicleType = String.IsNullOrEmpty(vehicleType) || String.IsNullOrWhiteSpace(vehicleType) ? defaultVehicleType : vehicleType.Trim();

            return $"{baseUrl}/{pathIcon}/{pathVehicleType}.png";
        }

        public IEnumerable<FleetIdentity> GetAllFleetsByUser(string loginName) 
        {
            var conn = db.Database.GetDbConnection();
            var fleetList = conn.Query<FleetIdentity>("dbo.UP_HAMS_GetFleetListByLoginName", 
                new { LoginName = loginName },
                commandType: CommandType.StoredProcedure);
            return fleetList;

        }

    }
}