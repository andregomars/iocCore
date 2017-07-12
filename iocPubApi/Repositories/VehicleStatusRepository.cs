using System;
using System.Collections.Generic;
using iocPubApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Dapper;
using Chilkat;
using System.Data;

namespace iocPubApi.Repositories
{
    public class VehicleStatusRepository : IVehicleStatusRepository
    {
        private readonly io_onlineContext db;
        private string folder;
        public IConfigurationRoot Configuration { get; }
        private ILogger<VehicleStatusRepository> _logger;

        public VehicleStatusRepository(io_onlineContext context,
            ILogger<VehicleStatusRepository> logger)
        {
            _logger = logger;
            db = context;
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            folder = Configuration["HAMS.Api:Directory.SMSData"];
        }

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

        }

       public IEnumerable<VehicleStatus> GetRecentAllByVehicleName(string vname)
        {
            var conn = db.Database.GetDbConnection();
            var statusList = conn.Query<VehicleStatus>("dbo.UP_HAMS_GetRecentVehicleStatusListByVehicle", 
                new { VehicleName = vname},
                commandType: CommandType.StoredProcedure);
            return statusList;
        }



        public IEnumerable<VehicleStatus> GetAllByFleetName(string fname)
        {
            var conn = db.Database.GetDbConnection();
            var statusList = conn.Query<VehicleStatus>("dbo.UP_HAMS_GetLatestVehicleStatusByFleet", 
                new { FleetName = fname},
                commandType: CommandType.StoredProcedure);
            return statusList;
        }

        public VehicleStatus GetByVehicleName(string vname)
        {
            var conn = db.Database.GetDbConnection();
            var statusList = conn.Query<VehicleStatus>("dbo.UP_HAMS_GetLatestVehicleStatusByVehicle", 
                new { VehicleName = vname},
                commandType: CommandType.StoredProcedure);
            return statusList.FirstOrDefault();
        }   

        public IEnumerable<VehicleStatus> GetWholeDayByVehicleName(string vname, DateTime date)
        {
            //two days ago
            if (date < DateTime.Today.AddDays(-1)) 
            {
                return GetWholeDayFromFile(vname, date);
            }
            //over today, return nothing  
            else if (date >= DateTime.Today.AddDays(1))
            {
                return new List<VehicleStatus>();
            }
            //today or yesterday
            else
            {
                return GetWholeDayFromDatabase(vname, date);
            }
        }

        private IEnumerable<VehicleStatus> GetWholeDayFromFile(string vname, DateTime date)
        {
            Csv csv = new Csv();
            csv.HasColumnNames = true;
            var statusList = new List<VehicleStatus>();

            // get file path from db, return nothing while no path is found
            string path = GetDailyFilePath(vname, date);
            if(String.IsNullOrEmpty(path)) return statusList;

            // load file as csv based on path, return nothing while no csv can be loaded
            bool success = csv.LoadFile(path);
            if(!success) return statusList; 

            int rowCount = csv.NumRows;
            int colCount = csv.NumColumns;
            for(int i = 0; i < rowCount; i++)
            {
                try {
                    statusList.Add(new VehicleStatus {
                        vid = 0,
                        vname = vname,
                        fid = 0,
                        fname = "",
                        lat = ParseGeoValue(csv.GetCellByName(i, "Latitude")),
                        lng = ParseGeoValue(csv.GetCellByName(i, "Longitude")),
                        axisx = ParseValue(csv.GetCellByName(i, "X")),
                        axisy = ParseValue(csv.GetCellByName(i, "Y")),
                        axisz = ParseValue(csv.GetCellByName(i, "Z")),
                        soc = ParseValue(csv.GetCellByName(i, "2A/SOC/%")),
                        status = ParseStatusValue(csv.GetCellByName(i, "2M/Left Charge Status/bit"),
                            csv.GetCellByName(i, "2N/Right Charge Status/bit")),
                        range = ParseValue(csv.GetCellByName(i, "2L/Range/miles")),
                        mileage = ParseValue(csv.GetCellByName(i, "2K/Total Mileage/miles")),
                        voltage = ParseValue(csv.GetCellByName(i, "2F/Total Voltage/V")),
                        temperaturehigh = ParseValue(csv.GetCellByName(i, "2H/Highest Battery Temp/F")),
                        temperaturelow = ParseValue(csv.GetCellByName(i, "2G/Lowest Battery Temp/F")),
                        speed = ParseValue(csv.GetCellByName(i, "2I/Speed/mph")),
                        remainingenergy = ParseValue(csv.GetCellByName(i, "2B/Battery Energy/kWh")),
                        updated = ParseDateValue(csv.GetCellByName(i, "Time"), date),
                    });
                }
                catch
                {
                    //when error when parsing a line, keep looping 
                    continue;
                }
            }

            return statusList;  
        }

        private IEnumerable<VehicleStatus> GetWholeDayFromDatabase(string vname, DateTime date)
        {
            var conn = db.Database.GetDbConnection();
            _logger.LogInformation("ready to UP_HAMS_GetWholeDayVehicleStatusByVehicle");
            IEnumerable<VehicleStatus> statusList = null;

            try {
                statusList = conn.Query<VehicleStatus>("dbo.UP_HAMS_GetWholeDayVehicleStatusByVehicle", 
                new { VehicleName = vname, Date = date.ToString("yyyy-MM-dd") },
                commandType: CommandType.StoredProcedure);
            } catch(Exception ex)
            {
                _logger.LogError(ex.StackTrace);
            }
            return statusList;
        }

        private string GetDailyFilePath(string vname, DateTime date)
        {
            /* equivalent T-SQL of the LINQ below 
            use io_online
            declare @vname varchar(50), @date DateTime
            set @vname = '3470'
            set @date = '2017-06-14'

            select [file] = rtrim(csv.filepath) + '\' + rtrim(csv.filename)
            from HAMS_CSV csv
            inner join IO_VEHICLE vehicle
                on csv.vehicleid = vehicle.vehicleid
            where vehicle.busno = @vname
            and csv.dailydate = @date
            */
            var path = from csv in db.HamsCsv
                        join vehicle in db.IoVehicle
                            on csv.VehicleId equals vehicle.VehicleId
                        where vehicle.BusNo == vname
                            && (csv.DailyDate??Convert.ToDateTime("1900-01-01")) == date
                        select $"{csv.FilePath.Trim()}\\{csv.FileName.Trim()}";
            return path.SingleOrDefault();
        }

        /*** Helper methods section ***/
        
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