using System;
using System.Collections.Generic;
using iocPubApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace iocPubApi.Repositories
{
    public class VehicleDailyUsageRepository : IVehicleDailyUsageRepository
    {
        private readonly io_onlineContext db;

        public VehicleDailyUsageRepository(io_onlineContext context)
        {
            db = context;
        }

        IEnumerable<VehicleDailyUsage> IVehicleDailyUsageRepository.GetByDateRange(string vname, DateTime beginDate, DateTime endDate)
        {
            string beginDay = beginDate.ToString("yyMMdd");
            string endDay = endDate.ToString("yyMMdd");

            /* equivalent T-SQL of the LINQ above
           use io_online
            declare @beginDay varchar(6)
                ,@endDay varchar(6)
            set @beginDay = '170503'
            set @endDay = '170505'
            select 
            vid = v.VehicleId 
            ,vname = v.BusNo
            ,fid = f.FleetId
            ,fname = f.Name
            ,date = m.YYMMDD
            ,mileage = m.Mileage
            ,soccharged = m.SOC_Charged
            ,socused = m.SOC_Used
            ,energycharged = m.kWh_Charged
            ,energyused = m.kWh_Used
            from [IO_DayTotal] m
            inner join IO_Vehicle v
                on m.VehicleId = v.VehicleId
            inner join IO_Fleet f
                on v.FleetId = f.FleetID
            where v.BusNo = '4003'
            and m.YYMMDD >= @beginDay
            and m.YYMMDD <= @endDay

             */
            IEnumerable<VehicleDailyUsage> usageList =
                from m in db.IoDayTotal
                    join v in db.IoVehicle
                        on m.VehicleId equals v.VehicleId
                    join f in db.IoFleet
                        on v.FleetId equals f.FleetId
                    where v.BusNo == vname
                        && DateTime.ParseExact(m.Yymmdd, "yyMMdd", null) >= beginDate 
                        && DateTime.ParseExact(m.Yymmdd, "yyMMdd", null) <= endDate 
                    select new VehicleDailyUsage
                    {
                        vid = v.VehicleId 
                        ,vname = v.BusNo
                        ,fid = f.FleetId
                        ,fname = f.Name
                        ,date = DateTime.ParseExact(m.Yymmdd, "yyMMdd", null)
                        ,mileage = m.Mileage?? 0
                        ,soccharged = m.SocCharged?? 0
                        ,socused = m.SocUsed?? 0
                        ,energycharged = m.KWhCharged?? 0
                        ,energyused = m.KWhUsed?? 0
                    };

            return usageList;
        }
    }
}