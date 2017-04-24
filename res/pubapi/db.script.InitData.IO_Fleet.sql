
truncate table dbo.IO_Fleet
set identity_insert dbo.IO_Fleet on

insert into dbo.IO_Fleet
(FleetID
,CompanyId
,UserName
,Name
,VehicleType
,IntervalSMS
,IntervalNet
,Timezone
,TimeOffset
,LogFormat
,LogStartTime
,LogEndTime
,Remark
,CreateTime
,Status)
select 1, 22, 'AVTA_admin', 'AVTA', 'Bus', 5, 5, -8, 0, 0, null, null, '', getdate(), 0
UNION ALL
select 2, 22, 'BYDUPS_admin', 'BYDUPS', 'Bus', 5, 5, -8, 0, 0, null, null, '', getdate(), 0
UNION ALL
select 3, 22, 'LACMTA_admin', 'LACMTA', 'Bus', 5, 5, -8, 0, 0, null, null, '', getdate(), 0
UNION ALL
select 4, 22, 'LBT_admin', 'LBT', 'Bus', 5, 5, -8, 0, 0, null, null, '', getdate(), 0

