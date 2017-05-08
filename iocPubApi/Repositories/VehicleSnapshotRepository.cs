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

        private IEnumerable<VehicleSnapshot> GetAllByDataId(IEnumerable<Guid> dataIdList)
        {
          /* equivalent T-SQL of the LINQ above
           use io_online
            select code = detail.ItemCode, 
                    name = detail.ItemName, 
                    value = detail.Value, 
                    unit = detail.Unit,
                    time = master.RealTime
            from [dataIdList] list
            inner join HAMS_SMSData master
                on list.DataId = master.DataId
            inner join HAMS_SMSItem detail
                on master.DataId = detail.DataId
           */
            var items = from list in dataIdList                                
                                join master in db.HamsSmsdata
                                    on list equals master.DataId
                                join detail in db.HamsSmsitem
                                    on master.DataId equals detail.DataId
                                select new VehicleSnapshot 
                                {  
                                    code = detail.ItemCode,
                                    name = detail.ItemName,
                                    value = detail.Value,
                                    unit = detail.Unit,
                                    time = master.RealTime?? Convert.ToDateTime("1900-01-01")
                                };            

            return items;
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

            return GetAllByDataId(dataIdList);
        }

                
        IEnumerable<VehicleSnapshot> IVehicleSnapshotRepository.GetWholeDayByVehicleName(string vname, DateTime date)
        {
            /* equivalent T-SQL of the LINQ above
            use io_online
            ;with dataIdList as
            (
                select m.DataId
                from HAMS_SMSData m
                inner join IO_Vehicle v
                    on m.VehicleId = v.VehicleId
                where v.BusNo = '4003'
                and m.RealTime >= @selectedDate 
                and m.RealTime < DATEADD(d,1, @selectedDate)
            )
           */
            var dataIdList = from m in db.HamsSmsdata
                        join v in db.IoVehicle
                            on m.VehicleId equals v.VehicleId
                        where v.BusNo == vname &&
                            m.RealTime >= date &&
                            m.RealTime < date.AddDays(1)
                        select m.DataId;

            return GetAllByDataId(dataIdList).OrderBy(row => row.time);  //ascending by time
        }
    }
}