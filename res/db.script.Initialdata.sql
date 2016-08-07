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
select 3,'iocro','iocro','internal','2',0,'8be4177df4ec5dee8c8bc4f3b49d7a2d','626-111-1111',null,'iocro@ioc.com',null,getdate(),'andre','Active'
UNION ALL
select 4,'iotest','iotest','byd','3',0,'8be4177df4ec5dee8c8bc4f3b49d7a2d','909-111-1111',null,'iotest@ioc.com',null,getdate(),'andre','Active'
UNION ALL
select 5,'ioc dbo','iocdbo','internal','1',1,'8be4177df4ec5dee8c8bc4f3b49d7a2d','626-222-2222',null,'iocdbo@ioc.com',null,getdate(),'andre','Active'
UNION ALL
select 6,'robin','robin','internal','1',1,'8be4177df4ec5dee8c8bc4f3b49d7a2d','626-222-2222',null,'robin@ioc.com',null,getdate(),'andre','Active'

set identity_insert dbo.core_user off


--Core_Role
set identity_insert dbo.Core_Role on

insert into Core_Role
(ID
,RoleName
,RoleType
,RoleDescription
,InDate
,InUser
,Status)
select 1,'administrator','internal','Administrator',getdate(),'andre','Active'
UNION ALL
select 2,'editor','internal','Editor',getdate(),'andre','Active'
UNION ALL
select 3,'author','internal','Author',getdate(),'andre','Active'
UNION ALL
select 4,'contributor','internal','Contributor',getdate(),'andre','Active'
UNION ALL
select 5,'subscriber','internal','Subscriber',getdate(),'andre','Active'

set identity_insert dbo.Core_Role off

/* copy from Core_Role_Bak

set identity_insert dbo.Core_Role on
truncate table Core_Role
insert into Core_Role
(ID
,RoleName
,RoleType
,RoleDescription
,InDate
,InUser
,Status
)
select 
ID
,RoleName
,RoleType
,RoleDescription
,InDate
,InUser
,Status
from Core_Role_Bak

set identity_insert dbo.Core_Role off

*/

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
,InDate
,InUser
,Status)
Select 'I/O Controls Corporation','Owned',getdate(),'iocdbo','Active'
UNION
Select 'LBT','Partner',getdate(),'iocdbo','Active'
UNION
Select 'BYD','Manufacturer',getdate(),'iocdbo','Active'


--Core_Function
--** refer to db.script.Initialdata.Core_Function.sql

--Core_Permission
--** refer to db.script.Initialdata.Core_Permission.sql


