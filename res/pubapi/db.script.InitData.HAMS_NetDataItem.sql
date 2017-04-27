USE [IO_Online]
GO

DROP TABLE [dbo].[HAMS_NetDataItem]
CREATE TABLE [dbo].[HAMS_NetDataItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DataId] [uniqueidentifier] NOT NULL,
	[DataType] [int] NOT NULL CONSTRAINT [DF__HAMS_NetD__DataT__01342732]  DEFAULT ((0)),
	[CanID] [int] NULL,
	[Data] [varbinary](8) NULL,
	[PGN] [varchar](4) NULL,
	[SPN] [int] NULL,
	[SPNName] [varchar](300) NULL,
	[Source] [int] NULL CONSTRAINT [DF__HAMS_NetD__Sourc__02284B6B]  DEFAULT ((0)),
	[Value] [float] NOT NULL CONSTRAINT [DF__HAMS_NetD__Value__031C6FA4]  DEFAULT ((0)),
	[StatusText] [varchar](30) NULL,
	[Unit] [varchar](10) NULL,
	[CreateTime] [datetime] NULL DEFAULT ([dbo].[IO_LocalNow_To_UTC]())
 CONSTRAINT [HAMS_NetDataItem_PK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
) ON [PRIMARY]
GO

INSERT INTO [dbo].[HAMS_NetDataItem] 
(DataId
,DataType
,CanID
,Data
,PGN
,SPN
,SPNName
,Source
,Value
,StatusText
,Unit
,CreateTime)
SELECT '9A30B6D9-8780-4269-BDAD-1C8A67317A7C', 0, null, null, null, 917, 'Total mileage', 0, 6237.9, null, 'miles', GETDATE()
UNION ALL
SELECT '9A30B6D9-8780-4269-BDAD-1C8A67317A7C', 0, null, null, null, 84, 'Vehicle speed', 0, 0, null, 'mph', GETDATE()
UNION ALL
SELECT '9A30B6D9-8780-4269-BDAD-1C8A67317A7C', 0, null, null, null, 9007, 'Range', 0, 253, null, 'miles', GETDATE()
UNION ALL
SELECT '9A30B6D9-8780-4269-BDAD-1C8A67317A7C', 0, null, null, null, 190, 'Engine speed', 0, 0, null, 'rpm', GETDATE()
UNION ALL
SELECT '9A30B6D9-8780-4269-BDAD-1C8A67317A7C', 0, null, null, null, 9012, 'Engine hour', 0, 317.7, null, 'h', GETDATE()
UNION ALL
SELECT '9A30B6D9-8780-4269-BDAD-1C8A67317A7C', 0, null, null, null, 9013, 'Idel time', 0, 95, null, 'h', GETDATE()
UNION ALL
SELECT '9A30B6D9-8780-4269-BDAD-1C8A67317A7C', 0, null, null, null, 9002, 'Voltage', 0, 25.6, null, 'V', GETDATE()
UNION ALL
SELECT '9A30B6D9-8780-4269-BDAD-1C8A67317A7C', 0, null, null, null, 9005, 'Engine temp', 0, 65, null, 'F', GETDATE()
UNION ALL
SELECT '9A30B6D9-8780-4269-BDAD-1C8A67317A7C', 0, null, null, null, 9006, 'Coolant temp', 0, 55, null, 'F', GETDATE()
UNION ALL
SELECT '9A30B6D9-8780-4269-BDAD-1C8A67317A7C', 0, null, null, null, 4001, 'SOC', 0, 80, null, 'A', GETDATE()
UNION ALL
SELECT '9A30B6D9-8780-4269-BDAD-1C8A67317A7C', 0, null, null, null, 4002, 'Current', 0, 20, null, 'A', GETDATE()
UNION ALL
SELECT '9A30B6D9-8780-4269-BDAD-1C8A67317A7C', 0, null, null, null, 4003, 'Remaining energy', 0, 12, null, 'kWh', GETDATE()

