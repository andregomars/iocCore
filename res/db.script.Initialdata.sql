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



--Core_Function
insert into Core_Function
(MenuID
,FunctionName
,FunctionDescription
,FunctionType
,Priority
,InDate
,InUser)
select 0,'manage_options','manage_options','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'edit_users','edit_users','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'edit_posts','edit_posts','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'read','read','WebFront',0,getdate(),'andre'

--Core_Permission
insert into Core_Permission
(RoleID
,FunctionID
,InDate
,InUser)
--administrator
select 1,1,getdate(),'andre'
UNION ALL
select 1,2,getdate(),'andre'
UNION ALL
select 1,3,getdate(),'andre'
UNION ALL
select 1,4,getdate(),'andre'
--editor
UNION ALL
select 2,2,getdate(),'andre'
UNION ALL
select 2,3,getdate(),'andre'
UNION ALL
select 2,4,getdate(),'andre'
--author
UNION ALL
select 3,3,getdate(),'andre'
UNION ALL
select 3,4,getdate(),'andre'
--contributor
UNION ALL
select 4,3,getdate(),'andre'
UNION ALL
select 4,4,getdate(),'andre'
--subscriber
UNION ALL
select 5,4,getdate(),'andre'


--Core_UserRole
insert into Core_UserRole
(UserID
,RoleID
,InDate
,InUser)
--administrator
select 3,1,getdate(),'andre'