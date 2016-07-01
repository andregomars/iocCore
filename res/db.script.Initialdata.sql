use core
go

insert into core_user(
Name
,LoginName
,UserType
,CompanyID
,IsAdmin
,Password
,CellPhone
,WorkPhone
,Email
,HeadImage
,InDate
,InUser
--,EditDate
--,EditUser
,Status
)
select 'andre shen','andre','internal','0',1,'1234','626-111-1111',null,'andregomars@gmail.com',null,getdate(),'andre','Active'
UNION ALL
select 'carol liu','carol','byd','29051',1,'1234','909-111-1111',null,'carol@gmail.com',null,getdate(),'andre','Active'
UNION ALL
select 'iocdbo','ioc dbo','internal','0',1,'1234','626-222-2222',null,'iocdbo@gmail.com',null,getdate(),'andre','Active'

