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

        VehicleStatus IVehicleStatusRepository.Get(string vname)
        {
            var db = _context;
            Guid detailID = (from detail in db.HamsNetDataItem
                                join master in db.HamsNetData
                                    on detail.DataId equals master.DataId
                                join vehicle in db.IoVehicle
                                    on master.VehicleId equals vehicle.VehicleId
                                where vehicle.BusNo.Trim() == vname
                                orderby master.CreateTime descending
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
                                    CreateTime = master.CreateTime
                                };

            VehicleStatus status = spnItems
                            .GroupBy(item => new { item.Vid, item.Vname, item.Fid, item.Fname, item.CreateTime })
                            .Select(group => new VehicleStatus 
                            {
                                vid = group.Key.Vid,
                                vname = group.Key.Vname,
                                fid = group.Key.Fid,
                                fname = group.Key.Fname,
                                updated = group.Key.CreateTime,
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