use io_online
go

CREATE proc UP_HAMS_GetFleetListByLoginName 
@LoginName varchar(20)
as

declare @usertype int

select top (1) @usertype = UserType
from dbo.IO_Users with(nolock)
where LogName = @LoginName

if @usertype in (2,4)
begin
	select distinct
        Fname = fleet.Name,
        VehicleType = fleet.VehicleType,
        Icon = 'http://52.35.12.17/online2017/hams/images/fleeticon/'+ 
            RTRIM(Icon) + '/' + RTRIM(VehicleType) + '.png'
    from IO_Vehicle vehicle with(nolock)
    inner join IO_Fleet fleet with(nolock)
        on vehicle.FleetId = fleet.FleetID
end
else if @usertype in (8,16,32,64)
begin
	select distinct
        Fname = fleet.Name,
        VehicleType = fleet.VehicleType,
        Icon = 'http://52.35.12.17/online2017/hams/images/fleeticon/'+ 
            RTRIM(Icon) + '/' + RTRIM(VehicleType) + '.png'
    from IO_Vehicle vehicle
    inner join IO_Fleet fleet with(nolock)
        on vehicle.FleetId = fleet.FleetID
	inner join IO_Users users with(nolock)
		on fleet.CompanyId = users.CompanyId
	where users.LogName = @LoginName
end
