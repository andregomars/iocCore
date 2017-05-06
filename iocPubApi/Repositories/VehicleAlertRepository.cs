using System;
using System.Collections.Generic;
using iocPubApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace iocPubApi.Repositories
{
    public class VehicleAlertRepository : IVehicleAlertRepository
    {
        private readonly io_onlineContext _context;

        public VehicleAlertRepository(io_onlineContext context)
        {
            _context = context;
        }

       IEnumerable<VehicleAlert> IVehicleAlertRepository.GetRecentAllByVehicleName(string vname)
        {
            var db = _context;
 
           /* equivalent T-SQL of the LINQ above
            use io_online
            ;with dataIdList as
            (select top (10) m.DataId 
            from HAMS_AlertData m
            inner join IO_Vehicle v
                on m.VehicleId = v.VehicleId
            where v.BusNo = '4370'
            order by m.DataTime)
           */ 
            var dataIdList = (from m in db.HamsAlertData
                        join v in db.IoVehicle
                            on m.VehicleId equals v.VehicleId
                        where v.BusNo == vname
                        orderby m.DataTime descending 
                        select new 
                        {
                            DataId = m.DataId
                        }).Take(10);
                                 
           /* equivalent T-SQL of the LINQ above
           use io_online
            select Vid = vehicle.VehicleId, 
                Vname = vehicle.BusNo,
                Fid = fleet.FleetId,
                Fname = fleet.Name,
                Code = detail.ItemCode,
                Name = detail.ItemName,
                Value = detail.Value,
                Unit = detail.Unit,
                DataTime = master.DataTime
            from [dataIdList] list
            inner join HAMS_AlertItem detail
                on list.DataId = detail.DataId
            inner join HAMS_AlertData master
                on detail.DataId = master.DataId
            inner join IO_Vehicle vehicle
                on master.VehicleId = vehicle.VehicleId
            inner join IO_Fleet fleet
                on vehicle.FleetId = fleet.FleetID
           */
            IEnumerable<VehicleAlert> alertList = from list in dataIdList
                                join detail in db.HamsAlertItem
                                    on list.DataId equals detail.DataId
                                join master in db.HamsAlertData
                                    on detail.DataId equals master.DataId
                                join vehicle in db.IoVehicle
                                    on master.VehicleId equals vehicle.VehicleId
                                join fleet in db.IoFleet
                                    on vehicle.FleetId equals fleet.FleetId
                                select new VehicleAlert 
                                {  
                                    vid = vehicle.VehicleId, 
                                    vname = vehicle.BusNo,
                                    fid = fleet.FleetId,
                                    fname = fleet.Name,
                                    code = detail.ItemCode,
                                    name = detail.ItemName,
                                    value = detail.Value,
                                    unit = detail.Unit,
                                    updated = master.DataTime
                                };

            return alertList;
        }

        IEnumerable<VehicleAlert> IVehicleAlertRepository.GetAllByFleetName(string fname)
        {
           throw new Exception("function GetAllByFleetName not implemented yet.");
        }

        VehicleAlert IVehicleAlertRepository.GetByVehicleName(string vname)
        {
           throw new Exception("function GetByVehicleName not implemented yet.");
        }
    }
}