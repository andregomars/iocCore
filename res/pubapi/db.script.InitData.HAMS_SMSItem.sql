USE [IO_Online]
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='HAMS_SMSItem') 
	DROP TABLE dbo.HAMS_SMSItem
GO

CREATE TABLE [dbo].[HAMS_SMSItem](
	[DataId] [uniqueidentifier] NOT NULL,
	[SMSId] [uniqueidentifier] NOT NULL,
	[VehicleId] [int] NOT NULL,
	[ItemCode] [varchar](2) NOT NULL,
	[Hex] [varchar](16) NULL,
	[ItemName] [varchar](300) NULL,
	[Data] [bigint] NOT NULL DEFAULT ((0)),
	[Value] [float] NOT NULL DEFAULT ((0)),
	[TextStatus] [varchar](20) NULL,
	[Unit] [varchar](20) NULL,
	[IsCondition] [smallint] NULL DEFAULT ((0)),
	[IsUpdate] [smallint] NOT NULL DEFAULT ((0)),
	[IsMoved] [smallint] NULL DEFAULT ((0)),
	[PGN] [varchar](6) NULL,
	[SPN] [smallint] NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [HAMS_SMSItem_PK] PRIMARY KEY CLUSTERED 
(
	[DataId] ASC, [ItemCode] ASC
)
) ON [PRIMARY]
GO


