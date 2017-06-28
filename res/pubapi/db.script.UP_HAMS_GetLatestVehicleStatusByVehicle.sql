CREATE  proc dbo.UP_HAMS_GetLatestVehicleStatusByVehicle 
@VehicleName nvarchar(50)
as

;with dataIdList as
(
	select top (1) m.DataId 
    from HAMS_SMSData m
    inner join IO_Vehicle v
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
inner join HAMS_SMSItem detail
    on list.DataId = detail.DataId
inner join HAMS_SMSData master
    on detail.DataId = master.DataId
inner join IO_Vehicle vehicle
    on master.VehicleId = vehicle.VehicleId
inner join IO_Fleet fleet
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



