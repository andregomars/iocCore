using System;
using System.Collections.Generic;
using iocPubApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace iocPubApi.Repositories
{
    public class VehicleSnapshotRepository : IVehicleSnapshotRepository
    {
        private readonly io_onlineContext db;

        public VehicleSnapshotRepository(io_onlineContext context)
        {
            db = context;
        }
        
        IEnumerable<VehicleSnapshot> IVehicleSnapshotRepository.GetByVehicleName(string vname)
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

          /* equivalent T-SQL of the LINQ above
           use io_online
            select vid = vehicle.VehicleId, 
                    vname = vehicle.BusNo,
                    fid = fleet.FleetId,
                    fname = fleet.Name,
                    code = detail.ItemCode, 
                    name = detail.ItemName, 
                    value = detail.Value, 
                    unit = detail.Unit 
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
            var items = from list in dataIdList
                                join detail in db.HamsSmsitem
                                    on list equals detail.DataId
                                join master in db.HamsSmsdata
                                    on detail.DataId equals master.DataId
                                join vehicle in db.IoVehicle
                                    on master.VehicleId equals vehicle.VehicleId
                                join fleet in db.IoFleet
                                    on vehicle.FleetId equals fleet.FleetId
                                select new VehicleSnapshot 
                                {  
                                    vid = vehicle.VehicleId, 
                                    vname = vehicle.BusNo,
                                    fid = fleet.FleetId,
                                    fname = fleet.Name,
                                    code = detail.ItemCode,
                                    name = detail.ItemName,
                                    value = detail.Value,
                                    unit = detail.Unit,
                                };            

            return items;
        }
    }
}