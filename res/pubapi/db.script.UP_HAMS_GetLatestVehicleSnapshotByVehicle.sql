use IO_Online
go

CREATE  proc dbo.UP_HAMS_GetLatestVehicleSnapshotByVehicle @VehicleName nvarchar(50)
as
;with dataIdList as
(
select top (1) m.DataId, m.RealTime
from HAMS_SMSData m with(nolock)
inner join IO_Vehicle v
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
