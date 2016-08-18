set identity_insert dbo.core_Role on

insert into Core_Role
(ID
,RoleName
,RoleType
,RoleDescription
,InDate
,InUser
)
select 1,'administrator', 'internal', 'Administrator', getdate(), 'andre'
UNION
select 2,'editor', 'internal', 'Editor', getdate(), 'andre'
UNION
select 3,'author', 'internal', 'Author', getdate(), 'andre'
UNION
select 4,'contributor', 'internal', 'Contributor', getdate(), 'andre'
UNION
select 5,'subscriber', 'internal', 'Subscriber', getdate(), 'andre'

set identity_insert dbo.core_Role off
