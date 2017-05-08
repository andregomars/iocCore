USE [IO_Online]
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='HAMS_SMSAlertData') 
	DROP TABLE dbo.HAMS_SMSAlertData
GO

CREATE TABLE [dbo].[HAMS_SMSAlertData](
	[DataId] [uniqueidentifier] NOT NULL,
	[VehicleId] [int] NOT NULL,
	[FileId] [int] NOT NULL,
	[CFGId] [int] NULL,
	[GPS] [int] NULL,
	[Lat] [varchar](50) NULL,
	[SN] [char](1) NULL,
	[Lng] [varchar](50) NULL,
	[EW] [char](1) NULL,
	[Accelerometer] [char](2) NULL,
	[AxisX] [char](5) NULL,
	[AxisY] [char](5) NULL,
	[AxisZ] [char](5) NULL,
	[DataTime] [datetime] NULL,
	[RealTime] [datetime] NULL,
	[IsUpdate] [smallint] NOT NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [HAMS_SMSAlertData_PK] PRIMARY KEY CLUSTERED 
(
	[DataId] ASC
)
) ON [PRIMARY]

GO
