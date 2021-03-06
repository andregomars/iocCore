use IO_Online
go

CREATE  proc dbo.UP_HAMS_GetLatestVehicleSnapshotByVehicle 
@VehicleName nvarchar(50)
as

;with dataIdList as
(
select top (1) m.DataId, m.RealTime, m.AxisX, m.AxisY, m.AxisZ
from HAMS_SMSData m with(nolock)
inner join IO_Vehicle v with(nolock)
    on m.VehicleId = v.VehicleId
where v.BusNo = @VehicleName
order by m.RealTime desc
)
select code = detail.ItemCode, 
        name = detail.ItemName, 
        value = detail.Value, 
        unit = detail.Unit,
        time = dataIdList.RealTime
from dataIdList
inner join HAMS_SMSItem detail with(nolock)
    on dataIdList.DataId = detail.DataId
UNION
select code = 'AX',
		name = 'AxisX',
		value = dataIdList.AxisX,
		unit = 'degree',
		time = dataIdList.RealTime
from dataIdList
UNION
select code = 'AY',
		name = 'AxisY',
		value = dataIdList.AxisY,
		unit = 'degree',
		time = dataIdList.RealTime
from dataIdList
UNION
select code = 'AZ',
		name = 'AxisZ',
		value = dataIdList.AxisZ,
		unit = 'degree',
		time = dataIdList.RealTime
from dataIdList