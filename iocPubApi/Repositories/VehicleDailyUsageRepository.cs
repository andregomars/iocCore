using System;
using System.Collections.Generic;
using iocPubApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace iocPubApi.Repositories
{
    public class VehicleDailyUsageRepository : IVehicleDailyUsageRepository, IDisposable
    {
        private readonly io_onlineContext db;

        public VehicleDailyUsageRepository(io_onlineContext context)
        {
            db = context;
        }

        IEnumerable<VehicleDailyUsage> IVehicleDailyUsageRepository.GetByDateRange(string vname, DateTime beginDate, DateTime endDate)
        {
            // string beginDay = beginDate.ToString("yyMMdd");
            // string endDay = endDate.ToString("yyMMdd");

            /* equivalent T-SQL of the LINQ above
            use io_online
            declare @beginDay varchar(6)
                ,@endDay varchar(6)
            set @beginDay = '170603'
            set @endDay = '170615'
            select 
            vid = v.VehicleId 
            ,vname = v.BusNo
            ,fid = f.FleetId
            ,fname = f.Name
            ,date = m.RealTime
            ,mileage = m.Mileage
            ,soccharged = m.SOC_Charged
            ,socused = m.SOC_Used
            ,energycharged = m.kWh_Charged
            ,energyused = m.kWh_Used
            from [HAMS_DayTotal] m
            inner join IO_Vehicle v
                on m.VehicleId = v.VehicleId
            inner join IO_Fleet f
                on v.FleetId = f.FleetID
            where v.BusNo = '3470'
			and m.RealTime >= @beginDay
            and m.RealTime <= @endDay

             */
            IEnumerable<VehicleDailyUsage> usageList =
                from m in db.HamsDayTotal
                    join v in db.IoVehicle
                        on m.VehicleId equals v.VehicleId
                    join f in db.IoFleet
                        on v.FleetId equals f.FleetId
                    where v.BusNo == vname
                        && m.RealTime >= beginDate
                        && m.RealTime <= endDate
                        // && DateTime.ParseExact(m.Yymmdd, "yyMMdd", null) >= beginDate 
                        // && DateTime.ParseExact(m.Yymmdd, "yyMMdd", null) <= endDate 
                    select new VehicleDailyUsage
                    {
                        vid = v.VehicleId 
                        ,vname = v.BusNo
                        ,fid = f.FleetId
                        ,fname = f.Name
                        // ,date = DateTime.ParseExact(m.Yymmdd, "yyMMdd", null)
                        ,date = m.RealTime?? Convert.ToDateTime("1900-01-01")
                        ,mileage = m.Mileage?? 0
                        ,soccharged = m.SocCharged?? 0
                        ,socused = m.SocUsed?? 0
                        ,energycharged = m.KWhCharged?? 0
                        ,energyused = m.KWhUsed?? 0
                    };

            return usageList;
        }

        public IEnumerable<VehicleDailyUsage> GetByFleet(string fname, DateTime beginDate, DateTime endDate)
        {
            /* equivalent T-SQL of the LINQ above
            use io_online
            declare @beginDay varchar(6)
                ,@endDay varchar(6)
            set @beginDay = '170603'
            set @endDay = '170615'
            select 
            vid = v.VehicleId 
            ,vname = v.BusNo
            ,fid = f.FleetId
            ,fname = f.Name
            ,date = m.RealTime
            ,mileage = m.Mileage
            ,soccharged = m.SOC_Charged
            ,socused = m.SOC_Used
            ,energycharged = m.kWh_Charged
            ,energyused = m.kWh_Used
            from [HAMS_DayTotal] m
            inner join IO_Vehicle v
                on m.VehicleId = v.VehicleId
            inner join IO_Fleet f
                on v.FleetId = f.FleetID
            where f.Name = 'avta'
			and m.RealTime >= @beginDay
            and m.RealTime <= @endDay

             */
            IEnumerable<VehicleDailyUsage> usageList =
                from m in db.HamsDayTotal
                    join v in db.IoVehicle
                        on m.VehicleId equals v.VehicleId
                    join f in db.IoFleet
                        on v.FleetId equals f.FleetId
                    where f.Name == fname 
                        && m.RealTime >= beginDate
                        && m.RealTime <= endDate
                    select new VehicleDailyUsage
                    {
                        vid = v.VehicleId 
                        ,vname = v.BusNo
                        ,fid = f.FleetId
                        ,fname = f.Name
                        ,date = m.RealTime?? Convert.ToDateTime("1900-01-01") 
                        ,mileage = m.Mileage?? 0
                        ,soccharged = m.SocCharged?? 0
                        ,socused = m.SocUsed?? 0
                        ,energycharged = m.KWhCharged?? 0
                        ,energyused = m.KWhUsed?? 0
                        ,soc_mile = (m.SocUsed?? 0) / ((m.Mileage?? 1) == 0 ? 1 : m.Mileage?? 1)
                        ,mile_soc = (m.Mileage?? 0) / ((m.SocUsed?? 1) == 0 ? 1 : m.SocUsed?? 1)
                        ,energy_mile = (m.KWhUsed?? 0) / ((m.Mileage?? 1) == 0 ? 1 : m.Mileage?? 1)
                        ,mile_energy = (m.Mileage?? 0) / ((m.KWhUsed?? 1) == 0 ? 1 : m.KWhUsed?? 1)
                    };

            return usageList;
        }

        public IEnumerable<VehicleDailyUsage> GetDaysSummaryByFleet(string fname, DateTime beginDate, DateTime endDate)
        {

            IEnumerable<VehicleDailyUsage> usageListDaily = GetByFleet(fname, beginDate, endDate);
            IEnumerable<VehicleDailyUsage> usageListDaysSummary = usageListDaily
                .GroupBy(item => new { item.vid, item.vname, item.fid, item.fname })
                .Select(group => new VehicleDailyUsage
                {
                    vid = group.Key.vid,
                    vname = group.Key.vname,
                    fid = group.Key.fid,
                    fname = group.Key.fname,
                    date = beginDate,
                    mileage = group.Sum(r => r.mileage),
                    soccharged = group.Sum(r => r.soccharged),
                    socused = group.Sum(r => r.socused),
                    energycharged = group.Sum(r => r.energycharged),
                    energyused = group.Sum(r => r.energyused),
                    soc_mile = group.Sum(r => r.socused) / (group.Sum(r => r.mileage) == 0 ? 1 : group.Sum(r => r.mileage)),
                    mile_soc = group.Sum(r => r.mileage) / (group.Sum(r => r.socused) == 0 ? 1 : group.Sum(r => r.socused)),
                    energy_mile = group.Sum(r => r.energyused) / (group.Sum(r => r.mileage) == 0 ? 1 : group.Sum(r => r.mileage)),
                    mile_energy = group.Sum(r => r.mileage) / (group.Sum(r => r.energyused) == 0 ? 1 : group.Sum(r => r.energyused)),
                });
            
            //attach total row
            VehicleDailyUsage usageTotalRow = usageListDaysSummary
                .GroupBy(item => new { item.fid, item.fname })
                .Select(group => new VehicleDailyUsage
                {
                    vid = 0,
                    vname = "All",
                    fid = group.Key.fid,
                    fname = group.Key.fname,
                    date = beginDate,
                    mileage = group.Sum(r => r.mileage),
                    soccharged = group.Sum(r => r.soccharged),
                    socused = group.Sum(r => r.socused),
                    energycharged = group.Sum(r => r.energycharged),
                    energyused = group.Sum(r => r.energyused),
                    soc_mile = group.Sum(r => r.socused) / (group.Sum(r => r.mileage) == 0 ? 1 : group.Sum(r => r.mileage)),
                    mile_soc = group.Sum(r => r.mileage) / (group.Sum(r => r.socused) == 0 ? 1 : group.Sum(r => r.socused)),
                    energy_mile = group.Sum(r => r.energyused) / (group.Sum(r => r.mileage) == 0 ? 1 : group.Sum(r => r.mileage)),
                    mile_energy = group.Sum(r => r.mileage) / (group.Sum(r => r.energyused) == 0 ? 1 : group.Sum(r => r.energyused)),
                }).SingleOrDefault();

            //concat and put total row in first line
            IEnumerable<VehicleDailyUsage> usageListDaysSummaryWithTotalRow = new VehicleDailyUsage[] { usageTotalRow };
            usageListDaysSummaryWithTotalRow = usageListDaysSummaryWithTotalRow.Concat(usageListDaysSummary);

            return usageListDaysSummaryWithTotalRow;
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