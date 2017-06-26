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
            /* equivalent T-SQL of the LINQ above
           use io_online
			select Vid = vehicle.VehicleId, 
                Vname = vehicle.BusNo,
                Fid = fleet.FleetId,
                Fname = fleet.Name,
				Lat = CASE master.SN 
					WHEN 'N' THEN (CASE ISNUMERIC(master.Lat) WHEN 1 THEN master.Lat ELSE 0 END) 
					ELSE (CASE ISNUMERIC(master.Lat) WHEN 1 THEN master.Lat ELSE 0 END) * -1 
					END,
				Lng = CASE master.EW WHEN 'E' THEN (CASE ISNUMERIC(master.Lng) WHEN 1 THEN master.Lng ELSE 0 END) 
					ELSE (CASE ISNUMERIC(master.Lng) WHEN 1 THEN master.Lng ELSE 0 END) * -1 
					END,
				AxisX = master.AxisX,
				AxisY = master.AxisY,
				AxisZ = master.AxisZ,
                ItemCode = detail.ItemCode,
                ItemName = detail.ItemName,
                Value = detail.Value,
                Unit = detail.Unit,
                RealTime = master.RealTime
            from [dataIdList] list
            inner join HAMS_SMSItem detail
                on list.DataId = detail.DataId
            inner join HAMS_SMSData master
                on detail.DataId = master.DataId
            inner join IO_Vehicle vehicle
                on master.VehicleId = vehicle.VehicleId
            inner join IO_Fleet fleet
                on vehicle.FleetId = fleet.FleetID
            group by vehicle.VehicleId,vehicle.BusNo,fleet.FleetID, fleet.Name
			    ,CASE master.SN 
					WHEN 'N' THEN (CASE ISNUMERIC(master.Lat) WHEN 1 THEN master.Lat ELSE 0 END) 
					ELSE (CASE ISNUMERIC(master.Lat) WHEN 1 THEN master.Lat ELSE 0 END) * -1 
					END
			    ,CASE master.EW WHEN 'E' THEN (CASE ISNUMERIC(master.Lng) WHEN 1 THEN master.Lng ELSE 0 END) 
					ELSE (CASE ISNUMERIC(master.Lng) WHEN 1 THEN master.Lng ELSE 0 END) * -1 
					END
			    ,AxisX,AxisY,AxisZ,detail.ItemCode, detail.ItemName, detail.Value, detail.Unit, RealTime
           */
            var statusList = (from list in dataIdList
                    join detail in db.HamsSmsitem
                        on list equals detail.DataId
                    join master in db.HamsSmsdata
                        on detail.DataId equals master.DataId
                    join vehicle in db.IoVehicle
                        on master.VehicleId equals vehicle.VehicleId
                    join fleet in db.IoFleet
                        on vehicle.FleetId equals fleet.FleetId
                    group new {vehicle, fleet, master, detail} 
                        by new 
                        {  
                            vid = vehicle.VehicleId, 
                            vname = vehicle.BusNo,
                            fid = fleet.FleetId,
                            fname = fleet.Name,
                            lat = master.Sn.Trim().Equals("N") ? ConvertGeoValue(master.Lat, false) : ConvertGeoValue(master.Lat, true),
                            lng = master.Ew.Trim().Equals("E") ? ConvertGeoValue(master.Lng, false) : ConvertGeoValue(master.Lng, true),
                            axisx = double.Parse(master.AxisX),
                            axisy = double.Parse(master.AxisY),
                            axisz = double.Parse(master.AxisZ),
                            // itemcode = detail.ItemCode,
                            // itemname = detail.ItemName,
                            // value = detail.Value,
                            // unit = detail.Unit,
                            realTime = master.RealTime
                        } into grp
                    select new VehicleStatus 
                {
                    vid = grp.Key.vid,
                    vname = grp.Key.vname,
                    fid = grp.Key.fid,
                    fname = grp.Key.fname,
                    lat = grp.Key.lat,
                    lng = grp.Key.lng,
                    axisx = grp.Key.axisx,
                    axisy = grp.Key.axisy,
                    axisz = grp.Key.axisz,
                    updated = grp.Key.realTime,
                    soc = grp.Where(row => row.itemcode.Equals("2A")).DefaultIfEmpty().Max(row => row == null ? 0 : row.value),
                    status = GetChargingStatus(Convert.ToInt32(grp.Where(row => row.itemcode.Equals("2M")).DefaultIfEmpty().Max(row => row == null ? 0 : row.value))
                        , Convert.ToInt32(grp.Where(row => row.itemcode.Equals("2N")).DefaultIfEmpty().Max(row => row == null ? 0 : row.value))),
                    range = grp.Where(row => row.itemcode.Equals("2L")).DefaultIfEmpty().Max(row => row == null ? 0 : row.value),
                    mileage = grp.Where(row => row.itemcode.Equals("2K")).DefaultIfEmpty().Max(row => row == null ? 0 : row.value),
                    voltage = grp.Where(row => row.itemcode.Equals("2F")).DefaultIfEmpty().Max(row => row == null ? 0 : row.value),
                    current = grp.Where(row => row.itemcode.Equals("2E")).DefaultIfEmpty().Max(row => row == null ? 0 : row.value),
                    temperaturehigh = grp.Where(row => row.itemcode.Equals("2H")).DefaultIfEmpty().Max(row => row == null ? 0 : row.value),
                    temperaturelow = grp.Where(row => row.itemcode.Equals("2G")).DefaultIfEmpty().Max(row => row == null ? 0 : row.value),
                    speed = grp.Where(row => row.itemcode.Equals("2I")).DefaultIfEmpty().Max(row => row == null ? 0 : row.value),
                    remainingenergy = grp.Where(row => row.itemcode.Equals("2C")).DefaultIfEmpty().Max(row => row == null ? 0 : row.value) 
                            - grp.Where(row => row.itemcode.Equals("2D")).DefaultIfEmpty().Max(row => row == null ? 0 : row.value)
                });
                // .OrderBy(status => status.vname);

            // _logger.LogInformation("start get list");
            //     var vidList = spnItems.Select(r => r.vid).Distinct().OrderBy(id => id);
            //     var statusList = new List<VehicleStatus>();
            //     foreach(var id in vidList)
            //     {
            //         var snapshotList = spnItems.Where(r => r.vid == id);
            //         var status = new VehicleStatus() 
            //         {
            //             vid = snapshotList.First().vid,
            //             vname = snapshotList.First().vname,
            //             fid = snapshotList.First().fid,
            //             fname = snapshotList.First().fname,
            //             lat = snapshotList.First().lat,
            //             lng = snapshotList.First().lng,
            //             axisx = snapshotList.First().axisx,
            //             axisy = snapshotList.First().axisy,
            //             axisz = snapshotList.First().axisz,
            //             updated = snapshotList.First().realTime
            //         };

            //         foreach(var snapshot in snapshotList)
            //         {
            //             switch(snapshot.itemcode.ToUpper())
            //             {
            //                 case "2A":
            //                     status.soc = snapshot.value;
            //                     break;
            //                 case "2M":
            //                     status.status = 1;
            //                     break;
            //                 case "2L":
            //                     status.range = snapshot.value;
            //                     break;
            //                 case "2K":
            //                     status.mileage = snapshot.value;
            //                     break;
            //                 default:
            //                     break;
            //             } 
            //         }
            //         statusList.Add(status);
            //     }
            // _logger.LogInformation("statusList count is "+statusList.Count);
            return statusList;
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