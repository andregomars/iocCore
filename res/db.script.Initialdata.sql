use core
go

--Core_User
set identity_insert dbo.core_user on

insert into core_user(
ID
,Name
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
,Status
)
select 3,'iocro','iocro','internal','0',1,'8be4177df4ec5dee8c8bc4f3b49d7a2d','626-111-1111',null,'iocro@ioc.com',null,getdate(),'andre','Active'
UNION ALL
select 4,'iotest','iotest','byd','29051',0,'8be4177df4ec5dee8c8bc4f3b49d7a2d','909-111-1111',null,'iotest@ioc.com',null,getdate(),'andre','Active'
UNION ALL
select 5,'ioc dbo','iocdbo','internal','0',1,'8be4177df4ec5dee8c8bc4f3b49d7a2d','626-222-2222',null,'iocdbo@ioc.com',null,getdate(),'andre','Active'
UNION ALL
select 6,'robin','robin','internal','0',1,'8be4177df4ec5dee8c8bc4f3b49d7a2d','626-222-2222',null,'robin@ioc.com',null,getdate(),'andre','Active'

set identity_insert dbo.core_user off


--Core_Role
insert into Core_Role
(RoleName
,RoleType
,RoleDescription
,InDate
,InUser
,Status)
select 'Administrator','internal','Administrator',getdate(),'andre','Active'
UNION ALL
select 'Editor','internal','Editor',getdate(),'andre','Active'
UNION ALL
select 'Author','internal','Author',getdate(),'andre','Active'
UNION ALL
select 'Contributor','internal','Contributor',getdate(),'andre','Active'
UNION ALL
select 'Subscriber','internal','Subscriber',getdate(),'andre','Active'


--Core_UserRole
insert into Core_UserRole
(UserID
,RoleID
,InDate
,InUser)
--administrator
select 3,1,getdate(),'andre'
UNION ALL
select 4,1,getdate(),'andre'
UNION ALL
select 5,1,getdate(),'andre'
UNION ALL
select 6,1,getdate(),'andre'

--Core_Function
--** refer to db.script.Initialdata.Core_Function.sql

--Core_Permission
--** refer to db.script.Initialdata.Core_Permission.sql


