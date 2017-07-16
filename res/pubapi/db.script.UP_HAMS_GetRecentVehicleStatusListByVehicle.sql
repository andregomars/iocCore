CREATE  proc dbo.UP_HAMS_GetRecentVehicleStatusListByVehicle 
@VehicleName nvarchar(50)
as

declare @MileageLastDay float
set @MileageLastDay = 0

--select top (1) @MileageLastDay = Value from HAMS_SMSItem with(nolock)
--where dataid = (select top (1) DataId from HAMS_SMSData with(nolock)
--where realTime < '2017-07-12'
--order by RealTime desc)
--and ItemCode = '2K'

;with dataIdList as
(
    select top (10) m.DataId 
    from HAMS_SMSData m with(nolock)
    inner join IO_Vehicle v with(nolock)
        on m.VehicleId = v.VehicleId
    where v.BusNo = @VehicleName
    order by m.RealTime desc
),
snapshotList as
(
select Vid = vehicle.VehicleId, 
    Vname = vehicle.BusNo,
    Fid = fleet.FleetId,
    Fname = fleet.Name,
    Vtype = fleet.VehicleType,
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
inner join HAMS_SMSItem detail with(nolock)
    on list.DataId = detail.DataId
inner join HAMS_SMSData master with(nolock)
    on detail.DataId = master.DataId
inner join IO_Vehicle vehicle with(nolock)
    on master.VehicleId = vehicle.VehicleId
inner join IO_Fleet fleet with(nolock)
    on vehicle.FleetId = fleet.FleetID
group by vehicle.VehicleId,vehicle.BusNo,fleet.FleetID, fleet.Name, fleet.VehicleType
    ,CASE master.SN 
        WHEN 'N' THEN (CASE ISNUMERIC(master.Lat) WHEN 1 THEN master.Lat ELSE 0 END) 
        ELSE (CASE ISNUMERIC(master.Lat) WHEN 1 THEN master.Lat ELSE 0 END) * -1 
        END
    ,CASE master.EW WHEN 'E' THEN (CASE ISNUMERIC(master.Lng) WHEN 1 THEN master.Lng ELSE 0 END) 
        ELSE (CASE ISNUMERIC(master.Lng) WHEN 1 THEN master.Lng ELSE 0 END) * -1 
        END
    ,AxisX,AxisY,AxisZ,detail.ItemCode, detail.ItemName, detail.Value, detail.Unit, RealTime
)
select vid, vname, fid, fname, vtype, lat, lng, axisx, axisy, axisz,
soc = ISNULL([2A], 0),
status = CASE WHEN ISNULL([2M],0) = 2 and ISNULL([2N],0) = 2 THEN 1 ELSE 0 END,
range = ISNULL([2L],0),
mileage = ISNULL([2K],0),
voltage = ISNULL([2F],0),
[current] = ISNULL([2E],0),
temperaturehigh = ISNULL([2H],0),
temperaturelow = ISNULL([2G],0),
speed = ISNULL([2I],0),
remainingenergy = ISNUll([2B],0),
highvoltagestatus = ISNUll([2U],0),
actualdistance = ISNULL([2K],0) - ISNULL(@MileageLastDay,0),
updated = realtime
from 
(SELECT vid,vname, fid, fname, UPPER(vtype) as vtype, lat, lng, axisx, axisy, axisz, realtime, ItemCode, Value   
    FROM snapshotList
) AS src  
PIVOT  
(  
AVG(Value)  
FOR ItemCode IN ([2A],[2B],[2C],[2D],[2E],[2F],[2G],[2H],[2I],[2J],[2K],[2L],[2M],[2N],[2T],[2U],[2Z])  
) AS pvt
order by updated desc

