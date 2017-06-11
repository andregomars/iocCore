using System;
using System.IO;
using System.Collections.Generic;
using iocPubApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace iocPubApi.Repositories
{
    public class FleetIdentityRepository : IFleetIdentityRepository
    {
        private readonly io_onlineContext _context;
        private string iconBaseUrl;
        public IConfigurationRoot Configuration { get; }

        public FleetIdentityRepository(io_onlineContext context)
        {
            _context = context;

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
            var db = _context;
            var fleets = (from vehicle in db.IoVehicle
                        join fleet in db.IoFleet
                            on vehicle.FleetId equals fleet.FleetId
                        select new FleetIdentity 
                        { 
                            Fname = fleet.Name,
                            VehicleType = fleet.VehicleType.Trim(),
                            Icon = GetIconUrl(fleet.Icon, fleet.VehicleType)
                        // }).Distinct();
                        }).GroupBy(r => new { r.Fname, r.VehicleType, r.Icon })
                            .Select(g => new FleetIdentity{
                                Fname = g.Key.Fname,
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
            /* equivalent T-SQL
  		    select distinct
                Fname = fleet.Name,
                VehicleType = fleet.VehicleType,
                Icon = 'http://52.35.12.17/online2017/hams/images/fleeticon/'+ 
                    RTRIM(Icon) + '/' + RTRIM(VehicleType) + '.png'
            from IO_Vehicle vehicle
            inner join IO_Fleet fleet
                on vehicle.FleetId = fleet.FleetID
			inner join IO_Users users
				on fleet.CompanyId = users.CompanyId
			where users.LogName = 'iocontrol'
             */
            var db = _context;
            IEnumerable<FleetIdentity> fleets;

            int? userType = (from users in db.IoUsers
                            where users.LogName == loginName
                            select users.UserType).SingleOrDefault();
            
            // return all fleets and vehicles when user is ioc user or admin 
            if (userType == 4 || userType == 2) 
                fleets = this.GetAllFleets(); 
            // return all vehicles when user is manufacturer or consumer
            else if (userType == 8 || userType == 16 || userType == 32 || userType == 64)
                fleets = (from vehicle in db.IoVehicle
                        join fleet in db.IoFleet
                            on vehicle.FleetId equals fleet.FleetId
                        join users in db.IoUsers
                            on fleet.CompanyId equals users.CompanyId
                        where users.LogName == loginName
                        select new FleetIdentity 
                        { 
                            Fname = fleet.Name,
                            VehicleType = fleet.VehicleType.Trim(),
                            Icon = GetIconUrl(fleet.Icon, fleet.VehicleType)
                        }).GroupBy(r => new { r.Fname, r.VehicleType, r.Icon })
                            .Select(g => new FleetIdentity{
                                Fname = g.Key.Fname,
                                VehicleType = g.Key.VehicleType,
                                Icon = g.Key.Icon
                            });
            else
            // return nothing when user not found in ioc user list, or no user type is found
                fleets = null;

            return fleets;
        }

    }
}