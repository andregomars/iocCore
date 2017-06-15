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
    public class VehicleStatusRepository : IVehicleStatusRepository
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
                                select new  
                                {  
                                    Vid = vehicle.VehicleId, 
                                    Vname = vehicle.BusNo,
                                    Fid = fleet.FleetId,
                                    Fname = fleet.Name,
                                    Lat = master.Sn.Trim().Equals("N") ? ConvertGeoValue(master.Lat, false) : ConvertGeoValue(master.Lat, true),
                                    Lng = master.Ew.Trim().Equals("E") ? ConvertGeoValue(master.Lng, false) : ConvertGeoValue(master.Lng, true),
                                    AxisX = double.Parse(master.AxisX),
                                    AxisY = double.Parse(master.AxisY),
                                    AxisZ = double.Parse(master.AxisZ),
                                    ItemCode = detail.ItemCode,
                                    ItemName = detail.ItemName,
                                    Value = detail.Value,
                                    Unit = detail.Unit,
                                    RealTime = master.RealTime
                                };

            /* Pivot the table */
            IEnumerable<VehicleStatus> statusList = spnItems
                            .GroupBy(item => new { item.Vid, item.Vname, item.Fid, item.Fname, 
                                item.Lat, item.Lng, item.AxisX, item.AxisY, item.AxisZ, item.RealTime })
                            .Select(group => new VehicleStatus 
                            {
                                vid = group.Key.Vid,
                                vname = group.Key.Vname,
                                fid = group.Key.Fid,
                                fname = group.Key.Fname,
                                lat = group.Key.Lat,
                                lng = group.Key.Lng,
                                axisx = group.Key.AxisX,
                                axisy = group.Key.AxisY,
                                axisz = group.Key.AxisZ,
                                updated = group.Key.RealTime,
                                soc = group.Where(row => row.ItemCode.Equals("2A")).DefaultIfEmpty().Max(row => row == null ? 0 : row.Value),
                                status = GetChargingStatus(Convert.ToInt32(group.Where(row => row.ItemCode.Equals("2M")).DefaultIfEmpty().Max(row => row == null ? 0 : row.Value))
                                    , Convert.ToInt32(group.Where(row => row.ItemCode.Equals("2N")).DefaultIfEmpty().Max(row => row == null ? 0 : row.Value))),
                                range = group.Where(row => row.ItemCode.Equals("2L")).DefaultIfEmpty().Max(row => row == null ? 0 : row.Value),
                                mileage = group.Where(row => row.ItemCode.Equals("2K")).DefaultIfEmpty().Max(row => row == null ? 0 : row.Value),
                                voltage = group.Where(row => row.ItemCode.Equals("2F")).DefaultIfEmpty().Max(row => row == null ? 0 : row.Value),
                                current = group.Where(row => row.ItemCode.Equals("2E")).DefaultIfEmpty().Max(row => row == null ? 0 : row.Value),
                                temperaturehigh = group.Where(row => row.ItemCode.Equals("2H")).DefaultIfEmpty().Max(row => row == null ? 0 : row.Value),
                                temperaturelow = group.Where(row => row.ItemCode.Equals("2G")).DefaultIfEmpty().Max(row => row == null ? 0 : row.Value),
                                speed = group.Where(row => row.ItemCode.Equals("2I")).DefaultIfEmpty().Max(row => row == null ? 0 : row.Value),
                                remainingenergy = group.Where(row => row.ItemCode.Equals("2C")).DefaultIfEmpty().Max(row => row == null ? 0 : row.Value) 
                                     - group.Where(row => row.ItemCode.Equals("2D")).DefaultIfEmpty().Max(row => row == null ? 0 : row.Value)
                            });

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

       IEnumerable<VehicleStatus> IVehicleStatusRepository.GetRecentAllByVehicleName(string vname)
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



        IEnumerable<VehicleStatus> IVehicleStatusRepository.GetAllByFleetName(string fname)
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
                where f.Name = 'HAMS06 test'
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

        VehicleStatus IVehicleStatusRepository.GetByVehicleName(string vname)
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

            string path = GetFilePath(vname, date);
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
           /* equivalent T-SQL of the LINQ above
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

        /*** Helper methods section ***/

        private string GetFilePath(string vname, DateTime date)
        {
            return $"{folder}\\{date.Year}\\{date.Month}\\{vname}_{date.ToString("yyyy-MM-dd")}.csv";
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