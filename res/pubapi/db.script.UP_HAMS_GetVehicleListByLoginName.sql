use io_online
go

CREATE proc UP_HAMS_GetVehicleListByLoginName 
@LoginName varchar(20)
as

declare @usertype int

select top (1) @usertype = UserType
from dbo.IO_Users with(nolock)
where LogName = @LoginName

if @usertype in (2,4)
begin
    select distinct
        Vid = vehicle.VehicleId, 
        Vname = vehicle.BusNo,
        Fid = fleet.FleetId,
        Fname = fleet.Name
    from IO_Vehicle vehicle with(nolock)
    inner join IO_Fleet fleet with(nolock)
        on vehicle.FleetId = fleet.FleetID
	order by fleet.Name, vehicle.BusNo
end
else if @usertype in (8,16,32,64)
begin
	select distinct
        Vid = vehicle.VehicleId, 
        Vname = vehicle.BusNo,
        Fid = fleet.FleetId,
        Fname = fleet.Name
    from IO_Users users with(nolock)
    inner join IO_Vehicle vehicle with(nolock)
        on users.CompanyId = vehicle.CompanyId
    inner join IO_Fleet fleet with(nolock)
        on vehicle.FleetId = fleet.FleetID
    where users.LogName = @LoginName
	order by fleet.Name, vehicle.BusNo
end
