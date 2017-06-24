using System;
using System.Collections.Generic;
using iocPubApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.IO;
using Microsoft.Extensions.Configuration;
using Chilkat;

namespace iocPubApi.Repositories
{
    public class VehicleStatusRepository : IVehicleStatusRepository, IDisposable
    {
        private readonly io_onlineContext db;
        private string folder;
        public IConfigurationRoot Configuration { get; }

        public VehicleStatusRepository(io_onlineContext context)
        {
            db = context;

            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            folder = Configuration["HAMS.Api:Directory.SMSData"];
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
			    ,AxisX,AxisY,AxisZ,RealTime
           */
            var spnItems = from list in dataIdList
                    join detail in db.HamsSmsitem
                        on list equals detail.DataId
                    join master in db.HamsSmsdata
                        on detail.DataId equals master.DataId
                    join vehicle in db.IoVehicle
                        on master.VehicleId equals vehicle.VehicleId
                    join fleet in db.IoFleet
                        on vehicle.FleetId equals fleet.FleetId
                    select new VehicleStatusWithCode
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
                        itemcode = detail.ItemCode,
                        itemname = detail.ItemName,
                        value = detail.Value,
                        unit = detail.Unit,
                        realTime = master.RealTime
                    };          

                // db.Dispose();
                // var vidList = spnItems.Select(r => r.vid).Distinct().OrderBy(id => id);
                // var statusList = new List<VehicleStatus>();
                // foreach(var id in vidList)
                // {
                //     var snapshotList = spnItems.Where(r => r.vid == id);
                //     var status = new VehicleStatus() 
                //     {
                //         vid = snapshotList.First().vid,
                //         vname = snapshotList.First().vname,
                //         fid = snapshotList.First().fid,
                //         fname = snapshotList.First().fname,
                //         lat = snapshotList.First().lat,
                //         lng = snapshotList.First().lng,
                //         axisx = snapshotList.First().axisx,
                //         axisy = snapshotList.First().axisy,
                //         axisz = snapshotList.First().axisz,
                //         updated = snapshotList.First().realTime
                //     };

                //     foreach(var snapshot in snapshotList)
                //     {
                //         switch(snapshot.itemcode.ToUpper())
                //         {
                //             case "2A":
                //                 status.soc = snapshot.value;
                //                 break;
                //             case "2M":
                //                 status.status = 1;
                //                 break;
                //             case "2L":
                //                 status.range = snapshot.value;
                //                 break;
                //             case "2K":
                //                 status.mileage = snapshot.value;
                //                 break;
                //             default:
                //                 break;
                //         } 
                //     }
                //     statusList.Add(status);
                // }

            /* Pivot the table */
            IEnumerable<VehicleStatus> statusList = spnItems
                .GroupBy(item => new { item.vid, item.vname, item.fid, item.fname, 
                    item.lat, item.lng, item.axisx, item.axisy, item.axisz, item.realTime })
                .Select(group => new VehicleStatus 
                {
                    vid = group.Key.vid,
                    vname = group.Key.vname,
                    fid = group.Key.fid,
                    fname = group.Key.fname,
                    lat = group.Key.lat,
                    lng = group.Key.lng,
                    axisx = group.Key.axisx,
                    axisy = group.Key.axisy,
                    axisz = group.Key.axisz,
                    updated = group.Key.realTime,
                    soc = group.Where(row => row.itemcode.Equals("2A")).DefaultIfEmpty().Max(row => row == null ? 0 : row.value),
                    status = GetChargingStatus(Convert.ToInt32(group.Where(row => row.itemcode.Equals("2M")).DefaultIfEmpty().Max(row => row == null ? 0 : row.value))
                        , Convert.ToInt32(group.Where(row => row.itemcode.Equals("2N")).DefaultIfEmpty().Max(row => row == null ? 0 : row.value))),
                    range = group.Where(row => row.itemcode.Equals("2L")).DefaultIfEmpty().Max(row => row == null ? 0 : row.value),
                    mileage = group.Where(row => row.itemcode.Equals("2K")).DefaultIfEmpty().Max(row => row == null ? 0 : row.value),
                    voltage = group.Where(row => row.itemcode.Equals("2F")).DefaultIfEmpty().Max(row => row == null ? 0 : row.value),
                    current = group.Where(row => row.itemcode.Equals("2E")).DefaultIfEmpty().Max(row => row == null ? 0 : row.value),
                    temperaturehigh = group.Where(row => row.itemcode.Equals("2H")).DefaultIfEmpty().Max(row => row == null ? 0 : row.value),
                    temperaturelow = group.Where(row => row.itemcode.Equals("2G")).DefaultIfEmpty().Max(row => row == null ? 0 : row.value),
                    speed = group.Where(row => row.itemcode.Equals("2I")).DefaultIfEmpty().Max(row => row == null ? 0 : row.value),
                    remainingenergy = group.Where(row => row.itemcode.Equals("2C")).DefaultIfEmpty().Max(row => row == null ? 0 : row.value) 
                            - group.Where(row => row.itemcode.Equals("2D")).DefaultIfEmpty().Max(row => row == null ? 0 : row.value)
                })
                .OrderBy(status => status.vname);

            return statusList;
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
            // if (double.TryParse(valueString, out val))
            //     return toNegative ? -val : val;
            // else
            //     return 0;
        }

       public IEnumerable<VehicleStatus> GetRecentAllByVehicleName(string vname)
        {
           /* equivalent T-SQL of the LINQ above
            use io_online
            ;with dataIdList as
            (
                select top (10) m.DataId 
                from HAMS_SMSData m
                inner join IO_Vehicle v
                    on m.VehicleId = v.VehicleId
                where v.BusNo = '4003'
                order by m.RealTime desc
			)
           */ 
            var dataIdList = (from m in db.HamsSmsdata
                        join v in db.IoVehicle
                            on m.VehicleId equals v.VehicleId
                        where v.BusNo == vname
                        orderby m.RealTime descending 
                        select m.DataId).Take(10);

           return GetAllByDataId(dataIdList);
        }



        public IEnumerable<VehicleStatus> GetAllByFleetName(string fname)
        {
           /* equivalent T-SQL of the LINQ above
            use io_online
            ;with dataIdList as
            (
			select b.dataid from
                (select m.VehicleId,max(RealTime) as RealTime 
                from HAMS_SMSData m
                inner join IO_Vehicle v
                    on m.VehicleId = v.VehicleId
                inner join IO_Fleet f
                    on v.FleetId = f.FleetID
                where f.Name = 'AVTA'
                group by m.VehicleId) a
            inner join HAMS_SMSData b 
            on a.VehicleId = b.VehicleId
                and a.RealTime = b.RealTime
			)
           */ 
            var dataIdList = from a in (from m in db.HamsSmsdata
                        join v in db.IoVehicle
                            on m.VehicleId equals v.VehicleId
                        join f in db.IoFleet
                            on v.FleetId equals f.FleetId
                        where f.Name == fname
                        group m by m.VehicleId into g
                        select new
                        {
                            VehicleId = g.Key,
                            RealTime = (from row in g select row.RealTime).Max()
                        }) join b in db.HamsSmsdata
                            on new { a.VehicleId, a.RealTime } equals new { b.VehicleId, b.RealTime }
                        select b.DataId;

            return GetAllByDataId(dataIdList);
        }

        public VehicleStatus GetByVehicleName(string vname)
        {
           /* equivalent T-SQL of the LINQ above
            use io_online
            ;with dataIdList as
            (
                select top (1) m.DataId 
                from HAMS_SMSData m
                inner join IO_Vehicle v
                    on m.VehicleId = v.VehicleId
                where v.BusNo = '4003'
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
           /* equivalent T-SQL of the LINQ below
            use io_online
            declare @vname varchar(50), @date DateTime
			set @vname = '3470'
			set @date = '2017-06-14'
            ;with dataIdList as
            (
                select m.DataId 
                from HAMS_SMSData m
                inner join IO_Vehicle v
                    on m.VehicleId = v.VehicleId
                where v.BusNo = @vname
                    and m.RealTime >= @date
                    and m.RealTime < DATEADD(day,1, @date)
                order by m.RealTime desc
		    )
           */ 
            var dataIdList = from m in db.HamsSmsdata
                        join v in db.IoVehicle
                            on m.VehicleId equals v.VehicleId
                        where v.BusNo == vname
                            && m.RealTime >= date
                            && m.RealTime < date.AddDays(1)
                        orderby m.RealTime descending 
                        select m.DataId;

           return GetAllByDataId(dataIdList);
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

        // private string GetFilePath(string vname, DateTime date)
        // {
        //     return $"{folder}\\{date.Year}\\{date.Month}\\{vname}_{date.ToString("yyyy-MM-dd")}.csv";
        // }

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