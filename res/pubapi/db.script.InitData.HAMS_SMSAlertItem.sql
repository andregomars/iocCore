USE [IO_Online]
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='HAMS_SMSAlertItem') 
	DROP TABLE dbo.HAMS_SMSAlertItem
GO

CREATE TABLE [dbo].[HAMS_SMSAlertItem](
	[DataId] [uniqueidentifier] NOT NULL,
	[SMSId] [uniqueidentifier] NOT NULL,
	[VehicleId] [int] NOT NULL,
	[ItemCode] [varchar](2) NOT NULL,
	[Hex] [varchar](16) NULL,
	[ItemName] [varchar](300) NULL,
	[Data] [bigint] NOT NULL,
	[Value] [float] NOT NULL,
	[TextStatus] [varchar](20) NULL,
	[Unit] [varchar](20) NULL,
	[IsCondition] [smallint] NULL,
	[IsUpdate] [smallint] NOT NULL,
	[PGN] [varchar](6) NULL,
	[SPN] [smallint] NULL,
	[CreateTime] [datetime] NULL
 CONSTRAINT [HAMS_SMSAlertItem_PK] PRIMARY KEY CLUSTERED 
(
	[DataId] ASC, [ItemCode] ASC
)
) ON [PRIMARY]

GO


