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
            throw new NotImplementedException();
        }

    }
}