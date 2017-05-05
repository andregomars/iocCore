using System;
using System.Collections.Generic;
using iocPubApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace iocPubApi.Repositories
{
    public class VehicleStatusRepository : IVehicleStatusRepository
    {
        private readonly io_onlineContext db;

        public VehicleStatusRepository(io_onlineContext context)
        {
            db = context;
        }

        private IEnumerable<VehicleStatus> GetAllByDataId(IEnumerable<Guid> dataIdList)
        {
            /* equivalent T-SQL of the LINQ above
           use io_online
			select Vid = vehicle.VehicleId, 
                Vname = vehicle.BusNo,
                Fid = fleet.FleetId,
                Fname = fleet.Name,
                Lng = master.Lng,
                Lat = master.Lat,
                ItemCode = detail.ItemCode,
                ItemName = detail.ItemName,
                Value = detail.Value,
                Unit = detail.Unit,
                DataTime = master.DataTime
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
                                    Lng = master.Lng,
                                    Lat = master.Lat,
                                    ItemCode = detail.ItemCode,
                                    ItemName = detail.ItemName,
                                    Value = detail.Value,
                                    Unit = detail.Unit,
                                    DataTime = master.DataTime
                                };

            /* Simply pivot the table */
            IEnumerable<VehicleStatus> statusList = spnItems
                            .GroupBy(item => new { item.Vid, item.Vname, item.Fid, item.Fname, item.Lat, item.Lng, item.DataTime })
                            .Select(group => new VehicleStatus 
                            {
                                vid = group.Key.Vid,
                                vname = group.Key.Vname,
                                fid = group.Key.Fid,
                                fname = group.Key.Fname,
                                lat = double.Parse(group.Key.Lat.Trim()),
                                lng = double.Parse(group.Key.Lng.Trim()),
                                updated = group.Key.DataTime,
                                soc = group.Where(row => row.ItemCode.Equals("1E")).Max(row => row.Value),
                                status = group.Where(row => row.ItemCode.Equals("1I")).Max(row => row.Value),
                                range = group.Where(row => row.ItemCode.Equals("2H")).Max(row => row.Value),
                                mileage = group.Where(row => row.ItemCode.Equals("1H")).Max(row => row.Value),
                                voltage = group.Where(row => row.ItemCode.Equals("1F")).Max(row => row.Value),
                                current = group.Where(row => row.ItemCode.Equals("2F")).Max(row => row.Value),
                                temperaturehigh = group.Where(row => row.ItemCode.Equals("2G")).Max(row => row.Value),
                                temperaturelow = group.Where(row => row.ItemCode.Equals("1G")).Max(row => row.Value),
                                speed = group.Where(row => row.ItemCode.Equals("1D")).Max(row => row.Value),
                                remainingenergy = group.Where(row => row.ItemCode.Equals("1J")).Max(row => row.Value)
                            });

            return statusList;
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
                order by m.DataTime desc
			)
           */ 
            var dataIdList = (from m in db.HamsSmsdata
                        join v in db.IoVehicle
                            on m.VehicleId equals v.VehicleId
                        where v.BusNo == vname
                        orderby m.DataTime descending 
                        select m.DataId).Take(10);
                        // select new 
                        // {
                        //     DataId = m.DataId
                        // }).Take(10);

           return GetAllByDataId(dataIdList);
                                 
  
        }

        IEnumerable<VehicleStatus> IVehicleStatusRepository.GetAllByFleetName(string fname)
        {
           /* equivalent T-SQL of the LINQ above
            use io_online
            ;with dataIdList as
            (
			select b.dataid from
                (select m.VehicleId,max(DataTime) as DataTime 
                from HAMS_SMSData m
                inner join IO_Vehicle v
                    on m.VehicleId = v.VehicleId
                inner join IO_Fleet f
                    on v.FleetId = f.FleetID
                where f.Name = 'HAMS06 test'
                group by m.VehicleId) a
            inner join HAMS_SMSData b 
            on a.VehicleId = b.VehicleId
                and a.DataTime = b.DataTime
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
                            DataTime = (from row in g select row.DataTime).Max()
                        }) join b in db.HamsSmsdata
                            on new { a.VehicleId, a.DataTime } equals new { b.VehicleId, b.DataTime }
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
                order by m.DataTime desc
			)
           */
            var dataIdList = (from m in db.HamsSmsdata
                        join v in db.IoVehicle
                            on m.VehicleId equals v.VehicleId
                        where v.BusNo == vname
                        orderby m.DataTime descending 
                        select m.DataId).Take(1);
            
            return GetAllByDataId(dataIdList).FirstOrDefault();
        }   
    }
}