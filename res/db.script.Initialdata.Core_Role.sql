set identity_insert dbo.core_Role on

insert into Core_Role
(ID
,RoleName
,RoleType
,RoleDescription
,InDate
,InUser
,Status
)
select 1,'administrator', 'internal', 'Administrator', getdate(), 'andre','Active'
UNION
select 2,'editor', 'internal', 'Editor', getdate(), 'andre','Active'
UNION
select 3,'author', 'internal', 'Author', getdate(), 'andre','Active'
UNION
select 4,'contributor', 'internal', 'Contributor', getdate(), 'andre','Active'
UNION
select 5,'subscriber', 'internal', 'Subscriber', getdate(), 'andre','Active'

set identity_insert dbo.core_Role off
