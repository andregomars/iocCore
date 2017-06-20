using System;
using System.Collections.Generic;
using iocPubApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace iocPubApi.Repositories
{
    public class VehicleDailyFileRepository : IVehicleDailyFileRepository
    {
        private readonly io_onlineContext db;

        public VehicleDailyFileRepository(io_onlineContext context)
        {
            db = context;
        }

        public IEnumerable<VehicleDailyFile> GetFileList(string[] vnames, DateTime beginDate, DateTime endDate)
        {
            string beginDay = beginDate.ToString("yyMMdd");
            string endDay = endDate.ToString("yyMMdd");

            /* equivalent T-SQL of the LINQ above
            use io_online
            declare @beginDay DATETIME
                ,@endDay DATETIME 
            declare @vnames table (BusNo NVARCHAR(50))
            set @beginDay = '2017-06-11'
            set @endDay = '2017-06-15'
            insert into @vnames (BusNo) values ('3470'),('3471')

            select 
            vid = vehicle.VehicleId 
            ,vname = vehicle.BusNo
            ,fid = fleet.FleetId
            ,fname = fleet.Name
            ,fileid = csv.CSVId
            ,filename = csv.FileName
            ,filetime = csv.DailyDate
            ,begintime = csv.StartTime
            ,endtime = csv.EndTime
            from HAMS_CSV csv
            inner join IO_Vehicle vehicle 
                on csv.VehicleId = vehicle.VehicleId
            inner join IO_Fleet fleet
                on vehicle.FleetId = fleet.FleetID
            inner join @vnames vnames
                on vehicle.BusNo = vnames.BusNo 
            where csv.StartTime >= @beginDay
            and csv.EndTime <= @endDay

             */
            IEnumerable<VehicleDailyFile> fileList =
                from csv in db.HamsCsv
                    join vehicle in db.IoVehicle
                        on csv.VehicleId equals vehicle.VehicleId
                    join fleet in db.IoFleet
                        on vehicle.FleetId equals fleet.FleetId
                    join names in vnames
                        on vehicle.BusNo.Trim() equals names
                    where csv.StartTime >= beginDate
                        && csv.EndTime <= endDate
                    orderby fleet.Name, csv.DailyDate
                    select new VehicleDailyFile
                    {
                        vid = vehicle.VehicleId 
                        ,vname = vehicle.BusNo
                        ,fid = fleet.FleetId
                        ,fname = fleet.Name
                        ,fileid = Convert.ToInt32(csv.Csvid)
                        ,filename = csv.FileName
                        ,filetime = csv.DailyDate?? Convert.ToDateTime("1900-01-01")
                        ,begintime = csv.StartTime?? Convert.ToDateTime("1900-01-01")
                        ,endtime = csv.EndTime?? Convert.ToDateTime("1900-01-01")
                    };

            return fileList;
        }

        public string GetDailyFilePath(int fileId)
        {
            /* equivalent T-SQL of the LINQ below 
            use io_online
            select [file] = rtrim(csv.filepath) + '\' + rtrim(csv.filename)
            from HAMS_CSV csv
            where csv.CSVId = 1
           */
            var path = from csv in db.HamsCsv
                        where csv.Csvid == fileId
                        select $"{csv.FilePath.Trim()}\\{csv.FileName.Trim()}";
            return path.SingleOrDefault();
        }

    }
}