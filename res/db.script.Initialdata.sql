use core
go

--Core_User
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
,Status
)
select 'andre shen','andre','internal','0',1,'1234','626-111-1111',null,'andregomars@gmail.com',null,getdate(),'andre','Active'
UNION ALL
select 'carol liu','carol','byd','29051',1,'1234','909-111-1111',null,'carol@gmail.com',null,getdate(),'andre','Active'
UNION ALL
select 'iocdbo','ioc dbo','internal','0',1,'1234','626-222-2222',null,'iocdbo@gmail.com',null,getdate(),'andre','Active'


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

--Core_Function
--** refer to db.script.Initialdata.Core_Function.sql

--Core_Permission
--** refer to db.script.Initialdata.Core_Permission.sql


