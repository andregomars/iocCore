using System;
using System.IO;
using System.Collections.Generic;
using iocPubApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace iocPubApi.Repositories
{
    public class PingRepository : IPingRepository
    {
        private readonly io_onlineContext db;
        public IConfigurationRoot Configuration { get; }
        private ILogger<PingRepository> _logger;

        public PingRepository(io_onlineContext context,
            ILogger<PingRepository> logger)
        {
            _logger = logger;
            db = context;
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
        }

        public string PingDB()
        {
            var fleetCount = db.IoFleet.Count();
            return $"Fleet count is {fleetCount}"; 
        }

        public string Reproduce()
        {
            string json = "";
            try
            {
                var resultObj = GetByVehicleName("3470");
                if (resultObj != null)
                    json = JsonConvert.SerializeObject(resultObj);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.StackTrace);
            }
            return json;            
        }
        
        private VehicleStatus GetByVehicleName(string vname)
        {
           /* equivalent T-SQL of the LINQ above
            use io_online
            ;with dataIdList as
            (
                select top (1) m.DataId 
                from HAMS_SMSData m
                inner join IO_Vehicle v
                    on m.VehicleId = v.VehicleId
                where v.BusNo = '3470'
                order by m.RealTime desc
			)
           */
            var dataIdList = (from m in db.HamsSmsdata
                        join v in db.IoVehicle
                            on m.VehicleId equals v.VehicleId
                        where v.BusNo == vname
                        orderby m.RealTime descending 
                        select m.DataId).Take(1);
            
            return GetAllByDataId(dataIdList).FirstOrDefault();
        }  

        private IEnumerable<VehicleStatus> GetAllByDataId(IEnumerable<Guid> dataIdList)
        {
            throw new NotImplementedException();
        }

        /*** Helper methods section ***/
        private double GetChargingStatus(double leftChargingStatus, double rightChargingStatus)
        {
            int valLeft = Convert.ToInt32(leftChargingStatus);
            int valRight = Convert.ToInt32(rightChargingStatus);
            //either left or right has status value of 2, it counts in charging status
            if ( valLeft == 2 || valRight == 2)
                return 1;
            else
                return 0;
        }

        private double ConvertGeoValue(string valueString, bool toNegative)
        {
            if (string.IsNullOrEmpty(valueString)) 
                return 0;
            
            //non-number would be in geo value from database, e.g. "FFFFFF"
            double val = double.TryParse(valueString, out val) ? val : 0;
            return toNegative ? -val : val;
            // if (double.TryParse(valueString, out val))
            //     return toNegative ? -val : val;
            // else
            //     return 0;
        }

        private double ParseGeoValue(string geoString)
        {
            if (String.IsNullOrEmpty(geoString) || geoString.Trim().ToUpper().Equals("N/A"))
                return 0;
            double val = double.TryParse(geoString, out val) ? val : 0;
            return val; 
        }

        private double ParseValue(string valString)
        {
            if (String.IsNullOrEmpty(valString) || String.IsNullOrWhiteSpace(valString))
                return 0;
            double val = double.TryParse(valString, out val) ? val : 0;
            return val;
        }

        private DateTime ParseDateValue(string dateString, DateTime date)
        {
            //when input datetime string is invalid, return the default datetime passed in
            if (String.IsNullOrEmpty(dateString) || String.IsNullOrWhiteSpace(dateString))
                return date; 
            DateTime val = DateTime.TryParse(dateString, out val) ? val : date;
            return val;
        }

        private double ParseStatusValue(string statusLeftStr, string statusRightStr)
        {
            double left = ParseValue(statusLeftStr);
            double right = ParseValue(statusRightStr);
            double val = (left == 2 && right == 2) ? 1 : 0;
            return val;
        }
    }
}