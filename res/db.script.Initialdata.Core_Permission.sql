--Core_Permission

--copy from Core_Permission_bak

set identity_insert dbo.Core_Permission on
truncate table Core_Permission
insert into Core_Permission
(ID
,RoleID
,FunctionID
,InDate
,InUser
,EditDate
,EditUser
,Status
)
select 
ID
,RoleID
,FunctionID
,InDate
,InUser
,EditDate
,EditUser
,Status
from Core_Permission_Bak

set identity_insert dbo.Core_Permission off

/*
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
UNION ALL
select 1,5,getdate(),'andre'
UNION ALL
select 1,6,getdate(),'andre'
UNION ALL
select 1,7,getdate(),'andre'
UNION ALL
select 1,8,getdate(),'andre'
UNION ALL
select 1,9,getdate(),'andre'
UNION ALL
select 1,10,getdate(),'andre'
UNION ALL
select 1,11,getdate(),'andre'
UNION ALL
select 1,12,getdate(),'andre'
UNION ALL
select 1,13,getdate(),'andre'
UNION ALL
select 1,14,getdate(),'andre'
UNION ALL
select 1,15,getdate(),'andre'
UNION ALL
select 1,16,getdate(),'andre'
UNION ALL
select 1,17,getdate(),'andre'
UNION ALL
select 1,18,getdate(),'andre'
UNION ALL
select 1,19,getdate(),'andre'
UNION ALL
select 1,20,getdate(),'andre'
UNION ALL
select 1,21,getdate(),'andre'
UNION ALL
select 1,22,getdate(),'andre'
UNION ALL
select 1,23,getdate(),'andre'
UNION ALL
select 1,24,getdate(),'andre'
UNION ALL
select 1,25,getdate(),'andre'
UNION ALL
select 1,26,getdate(),'andre'
UNION ALL
select 1,27,getdate(),'andre'
UNION ALL
select 1,28,getdate(),'andre'
UNION ALL
select 1,29,getdate(),'andre'
UNION ALL
select 1,30,getdate(),'andre'
UNION ALL
select 1,31,getdate(),'andre'
UNION ALL
select 1,32,getdate(),'andre'
UNION ALL
select 1,33,getdate(),'andre'
UNION ALL
select 1,34,getdate(),'andre'
UNION ALL
select 1,35,getdate(),'andre'
UNION ALL
select 1,36,getdate(),'andre'
UNION ALL
select 1,37,getdate(),'andre'
UNION ALL
select 1,38,getdate(),'andre'
UNION ALL
select 1,39,getdate(),'andre'
UNION ALL
select 1,40,getdate(),'andre'
UNION ALL
select 1,41,getdate(),'andre'
UNION ALL
select 1,42,getdate(),'andre'
UNION ALL
select 1,43,getdate(),'andre'
UNION ALL
select 1,44,getdate(),'andre'
UNION ALL
select 1,45,getdate(),'andre'
UNION ALL
select 1,46,getdate(),'andre'
UNION ALL
select 1,47,getdate(),'andre'
UNION ALL
select 1,48,getdate(),'andre'
UNION ALL
select 1,49,getdate(),'andre'
UNION ALL
select 1,50,getdate(),'andre'
UNION ALL
select 1,51,getdate(),'andre'
UNION ALL
select 1,52,getdate(),'andre'
UNION ALL
select 1,53,getdate(),'andre'
UNION ALL
select 1,54,getdate(),'andre'
UNION ALL
select 1,55,getdate(),'andre'
UNION ALL
select 1,56,getdate(),'andre'
UNION ALL
select 1,57,getdate(),'andre'
UNION ALL
select 1,58,getdate(),'andre'
UNION ALL
select 1,59,getdate(),'andre'
UNION ALL
select 1,60,getdate(),'andre'
UNION ALL
select 1,61,getdate(),'andre'
UNION ALL
select 1,62,getdate(),'andre'
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
*/