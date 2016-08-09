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
,Gender
,Password
,Tel
,Mobile
,Email
,HeadImage
,ValidDate
,IsActive
,InDate
,InUser
,Status
)
select 3,'iocro','iocro',4,21,0,'8be4177df4ec5dee8c8bc4f3b49d7a2d','626-111-1111',null,'iocro@ioc.com','01',getdate(),1,getdate(),'andre',0
UNION ALL
select 4,'iotest','iotest',4,21,0,'8be4177df4ec5dee8c8bc4f3b49d7a2d','909-111-1111',null,'iotest@ioc.com','01',getdate(),1,getdate(),'andre',0
UNION ALL
select 5,'ioc dbo','iocdbo',4,21,1,'8be4177df4ec5dee8c8bc4f3b49d7a2d','626-222-2222',null,'iocdbo@ioc.com','01',getdate(),1,getdate(),'andre',0
UNION ALL
select 6,'robin','robin',4,21,1,'8be4177df4ec5dee8c8bc4f3b49d7a2d','626-222-2222',null,'robin@ioc.com','01',getdate(),1,getdate(),'andre',0

set identity_insert dbo.core_user off


--Core_Role
set identity_insert dbo.Core_Role on

insert into Core_Role
(ID
,RoleName
,RoleType
,RoleDescription
,InDate
,InUser)
select 1,'administrator','internal','Administrator',getdate(),'andre'
UNION ALL
select 2,'editor','internal','Editor',getdate(),'andre'
UNION ALL
select 3,'author','internal','Author',getdate(),'andre'
UNION ALL
select 4,'contributor','internal','Contributor',getdate(),'andre'
UNION ALL
select 5,'subscriber','internal','Subscriber',getdate(),'andre'

set identity_insert dbo.Core_Role off


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

--Core_UserCompany
insert into Core_Company
(Name
,CompanyType
,IsStop
,InDate
,InUser)
Select 'I/O Controls Corporation',2,0,getdate(),'iocdbo'
UNION
Select 'LBT',4,0,getdate(),'iocdbo'
UNION
Select 'BYD',6,0,getdate(),'iocdbo'


--Core_Function
--** refer to db.script.Initialdata.Core_Function.sql

--Core_Permission
--** refer to db.script.Initialdata.Core_Permission.sql


