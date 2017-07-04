create  proc dbo.UP_HAMS_GetLatestVehicleStatusByFleet @FleetName nvarchar(100)
as

;with dataIdList as
(
	select b.dataid from
        (select m.VehicleId,max(RealTime) as RealTime 
        from HAMS_SMSData m with(nolock)
        inner join IO_Vehicle v with(nolock)
            on m.VehicleId = v.VehicleId
        inner join IO_Fleet f with(nolock)
            on v.FleetId = f.FleetID
        where f.Name = @FleetName
        group by m.VehicleId) a
    inner join HAMS_SMSData b with(nolock)
    on a.VehicleId = b.VehicleId
        and a.RealTime = b.RealTime
),
snapshotList as
(
select Vid = vehicle.VehicleId, 
    Vname = vehicle.BusNo,
    Fid = fleet.FleetId,
    Fname = fleet.Name,
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
group by vehicle.VehicleId,vehicle.BusNo,fleet.FleetID, fleet.Name
	,CASE master.SN 
		WHEN 'N' THEN (CASE ISNUMERIC(master.Lat) WHEN 1 THEN master.Lat ELSE 0 END) 
		ELSE (CASE ISNUMERIC(master.Lat) WHEN 1 THEN master.Lat ELSE 0 END) * -1 
		END
	,CASE master.EW WHEN 'E' THEN (CASE ISNUMERIC(master.Lng) WHEN 1 THEN master.Lng ELSE 0 END) 
		ELSE (CASE ISNUMERIC(master.Lng) WHEN 1 THEN master.Lng ELSE 0 END) * -1 
		END
	,AxisX,AxisY,AxisZ,detail.ItemCode, detail.ItemName, detail.Value, detail.Unit, RealTime
)
select vid, vname, fid, fname, lat, lng, axisx, axisy, axisz,
soc = ISNULL([2A], 0),
status = CASE WHEN ISNULL([2M],0) = 2 and ISNULL([2N],0) = 2 THEN 1 ELSE 0 END,
range = ISNULL([2L],0),
mileage = ISNULL([2K],0),
voltage = ISNULL([2F],0),
[current] = ISNULL([2E],0),
temperaturehigh = ISNULL([2H],0),
temperaturelow = ISNULL([2G],0),
speed = ISNULL([2I],0),
remainingenergy = ISNUll([2C],0) - ISNULL([2D],0),
updated = realtime
from 
(SELECT vid,vname, fid, fname, lat, lng, axisx, axisy, axisz, realtime, ItemCode, Value   
    FROM snapshotList
) AS src  
PIVOT  
(  
AVG(Value)  
FOR ItemCode IN ([2A],[2B],[2C],[2D],[2E],[2F],[2G],[2H],[2I],[2J],[2K],[2L],[2M],[2N],[2T],[2U],[2Z])  
) AS pvt
order by pvt.Vname
