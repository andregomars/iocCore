using System;
using System.Collections.Generic;
using iocPubApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace iocPubApi.Repositories
{
    public class VehicleStatusRepository : IVehicleStatusRepository
    {
        private readonly io_onlineContext _context;

        public VehicleStatusRepository(io_onlineContext context)
        {
            _context = context;
        }

        IEnumerable<VehicleStatus> IVehicleStatusRepository.GetAllByFleetName(string fname)
        {
            var db = _context;
 
           /* equivalent T-SQL of the LINQ above
            use io_online
            ;with dataIdList as
            (select b.dataid from
                (select m.VehicleId,max(DataTime) as DataTime 
                from HAMS_NetData m
                inner join IO_Vehicle v
                    on m.VehicleId = v.VehicleId
                inner join IO_Fleet f
                    on v.FleetId = f.FleetID
                where f.Name = 'AVTA'
                group by m.VehicleId) a
            inner join HAMS_NetData b 
            on a.VehicleId = b.VehicleId
                and a.DataTime = b.DataTime)
           */ 
            var dataIdList = from a in (from m in db.HamsNetData
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
                        }) join b in db.HamsNetData
                            on new { a.VehicleId, a.DataTime } equals new {b.VehicleId, b.DataTime}
                        select new 
                        {
                            DataId = b.DataId
                        };
                                 
           /* equivalent T-SQL of the LINQ above
           use io_online
            select Vid = vehicle.VehicleId, 
                Vname = vehicle.BusNo,
                Fid = fleet.FleetId,
                Fname = fleet.Name,
                Lng = master.Lng,
                Lat = master.Lat,
                SPN = detail.Spn,
                SPNname = detail.Spnname,
                Value = detail.Value,
                Unit = detail.Unit
            from [dataIdList] list
            inner join HAMS_NetDataItem detail
                on list.DataId = detail.DataId
            inner join HAMS_NetData master
                on detail.DataId = master.DataId
            inner join IO_Vehicle vehicle
                on master.VehicleId = vehicle.VehicleId
            inner join IO_Fleet fleet
                on vehicle.FleetId = fleet.FleetID
           */
            var spnItems = from list in dataIdList
                                join detail in db.HamsNetDataItem
                                    on list.DataId equals detail.DataId
                                join master in db.HamsNetData
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
                                    SPN = detail.Spn,
                                    SPNname = detail.Spnname,
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
                                soc = group.Where(row => row.SPN == 4001).Max(row => row.Value),
                                status = group.Where(row => row.SPN == 4004).Max(row => row.Value),
                                range = group.Where(row => row.SPN == 9007).Max(row => row.Value),
                                mileage = group.Where(row => row.SPN == 917).Max(row => row.Value),
                                voltage = group.Where(row => row.SPN == 9002).Max(row => row.Value),
                                current = group.Where(row => row.SPN == 4002).Max(row => row.Value),
                                temperaturehigh = group.Where(row => row.SPN == 9005).Max(row => row.Value),
                                temperaturelow = group.Where(row => row.SPN == 9006).Max(row => row.Value),
                                speed = group.Where(row => row.SPN == 84).Max(row => row.Value),
                                remainingenergy = group.Where(row => row.SPN == 4003).Max(row => row.Value)
                            });

            return statusList;
        }

        VehicleStatus IVehicleStatusRepository.GetByVehicleName(string vname)
        {
            var db = _context;
            Guid detailID = (from detail in db.HamsNetDataItem
                                join master in db.HamsNetData
                                    on detail.DataId equals master.DataId
                                join vehicle in db.IoVehicle
                                    on master.VehicleId equals vehicle.VehicleId
                                where vehicle.BusNo.Trim() == vname
                                orderby master.DataTime descending
                                select detail.DataId).FirstOrDefault();
            if (detailID == Guid.Empty) return null;
            
            var spnItems = from detail in db.HamsNetDataItem
                                join master in db.HamsNetData
                                    on detail.DataId equals master.DataId
                                join vehicle in db.IoVehicle
                                    on master.VehicleId equals vehicle.VehicleId
                                join fleet in db.IoFleet
                                    on vehicle.FleetId equals fleet.FleetId
                                where detail.DataId == detailID
                                select new 
                                {  
                                    Vid = vehicle.VehicleId, 
                                    Vname = vehicle.BusNo,
                                    Fid = fleet.FleetId,
                                    Fname = fleet.Name,
                                    Lng = master.Lng,
                                    Lat = master.Lat,
                                    SPN = detail.Spn,
                                    SPNname = detail.Spnname,
                                    Value = detail.Value,
                                    Unit = detail.Unit,
                                    DataTime = master.DataTime
                                };

            /* Simply pivot the table */
            VehicleStatus status = spnItems
                            .GroupBy(item => new { item.Vid, item.Vname, item.Fid, item.Fname, item.Lat, item.Lng, item.DataTime })
                            .Select(group => new VehicleStatus 
                            {
                                vid = group.Key.Vid,
                                vname = group.Key.Vname,
                                fid = group.Key.Fid,
                                fname = group.Key.Fname,
                                updated = group.Key.DataTime,
                                lat = double.Parse(group.Key.Lat.Trim()),
                                lng = double.Parse(group.Key.Lng.Trim()),
                                soc = group.Where(row => row.SPN == 4001).Max(row => row.Value),
                                status = group.Where(row => row.SPN == 4004).Max(row => row.Value),
                                range = group.Where(row => row.SPN == 9007).Max(row => row.Value),
                                mileage = group.Where(row => row.SPN == 917).Max(row => row.Value),
                                voltage = group.Where(row => row.SPN == 9002).Max(row => row.Value),
                                current = group.Where(row => row.SPN == 4002).Max(row => row.Value),
                                temperaturehigh = group.Where(row => row.SPN == 9005).Max(row => row.Value),
                                temperaturelow = group.Where(row => row.SPN == 9006).Max(row => row.Value),
                                speed = group.Where(row => row.SPN == 84).Max(row => row.Value),
                                remainingenergy = group.Where(row => row.SPN == 4003).Max(row => row.Value)
                            })
                            .FirstOrDefault();

            return status;
        }
    }
}