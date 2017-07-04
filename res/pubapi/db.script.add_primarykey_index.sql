use io_online
go

--primary key
alter table IO_Users
add CONSTRAINT [IO_Users_PK] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)
go

alter table IO_Fleet
add CONSTRAINT [IO_Fleet_PK] PRIMARY KEY CLUSTERED 
(
	[FleetID] ASC
)
go

alter table IO_Vehicle
add CONSTRAINT [IO_Vehicle_PK] PRIMARY KEY CLUSTERED 
(
	[VehicleId] ASC
)
go

alter table HAMS_SMSData
add CONSTRAINT [HAMS_HAMS_SMSData_PK] PRIMARY KEY CLUSTERED 
(
	[DataId] ASC
)
go

alter table HAMS_SMSItem
add CONSTRAINT [HAMS_SMSItem_PK] PRIMARY KEY CLUSTERED 
(
	[DataId] ASC, [ItemCode] ASC
)
go


--index
CREATE INDEX IDX_IO_Users_LogName
ON IO_Users (LogName)
go

CREATE INDEX IDX_IO_Vehicle_BusNo
ON IO_Vehicle (BusNo)
go

CREATE INDEX IDX_IO_Fleet_Name
ON IO_Fleet (Name)
go

CREATE INDEX IDX_HAMS_SMSData_VehicleId
ON HAMS_SMSData (DataId)
go

CREATE INDEX IDX_HAMS_SMSItem_DataId
ON HAMS_SMSItem (DataId)
go

