USE [IO_Online]
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='HAMS_SMSData') 
	DROP TABLE dbo.HAMS_SMSData
GO


CREATE TABLE [dbo].[HAMS_SMSData](
	[DataId] [uniqueidentifier] NOT NULL,
	[VehicleId] [int] NOT NULL,
	[FileId] [int] NULL,
	[CFGId] [int] NULL,
	[GPS] [int] NULL DEFAULT ((0)),
	[Lat] [varchar](50) NULL,
	[SN] [char](1) NULL DEFAULT (NULL),
	[Lng] [varchar](50) NULL,
	[EW] [char](1) NULL DEFAULT (NULL),
	[Accelerometer] [char](2) NULL,
	[AxisX] [char](5) NULL,
	[AxisY] [char](5) NULL,
	[AxisZ] [char](5) NULL,
	[DataTime] [datetime] NULL,
	[RealTime] [datetime] NULL,
	[IsUpdate] [smallint] NOT NULL DEFAULT ((0)),
	[IsMoved] [smallint] NULL DEFAULT ((0)),
	[IsDone] [smallint] NULL DEFAULT ((0)),
	[CreateTime] [datetime] NULL,
 CONSTRAINT [HAMS_HAMS_SMSData_PK] PRIMARY KEY CLUSTERED 
(
	[DataId] ASC
)
) ON [PRIMARY]
GO

