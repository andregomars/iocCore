use Core
go

select role.RoleName 
from Core_User [user]
inner join Core_UserRole as userRole
	on [user].ID = userRole.UserID
inner join Core_Role as role
	on userRole.RoleID = role.ID
where [user].LoginName = 'iotest'