USE [IO_Online]
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='IO_Vehicle') 
	DROP TABLE dbo.IO_Vehicle
GO

CREATE TABLE [dbo].[IO_Vehicle](
	[VehicleId] [int] IDENTITY(1,1) NOT NULL,
	[FleetId] [int] NULL,
	[CompanyId] [int] NOT NULL,
	[BuilderId] [int] NULL,
	[UserId] [int] NOT NULL,
	[BusNo] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[MAC] [nvarchar](50) NOT NULL,
	[VINNO] [varchar](30) NULL,
	[TypeId] [smallint] NULL,
	[Online] [smallint] NOT NULL CONSTRAINT [DF__IO_Vehicl__Onlin__63A3C44B]  DEFAULT ((0)),
	[OnlineTime] [datetime] NULL,
	[Monitor] [smallint] NULL,
	[CFG_FileId] [int] NULL,
	[Remark] [varchar](50) NULL,
	[CreateTime] [datetime] NOT NULL,
	[Status] [int] NULL,
 CONSTRAINT [IO_Vehicle_PK] PRIMARY KEY CLUSTERED 
(
	[VehicleId] ASC
)
) ON [PRIMARY]
GO

set identity_insert dbo.IO_Vehicle on

insert into dbo.IO_Vehicle
(VehicleId
,FleetId
,CompanyId
,BuilderId
,UserId
,BusNo
,Phone
,MAC
,VINNO
,TypeId
,Online
,OnlineTime
,Monitor
,CFG_FileId
,Remark
,CreateTime
,Status)
SELECT 1, 1, 22, null, 33, '4370', '13182939966', '20:F8:5E:C9:3D:10', null, null, 0, null, null, null, '', getdate(), 0
UNION ALL
SELECT 2, 1, 22, null, 33, '4371', '13182939966', '20:F8:5E:C9:3D:10', null, null, 0, null, null, null, '', getdate(), 0
UNION ALL
SELECT 3, 2, 22, null, 33, 'UPS', '13182939966', '20:F8:5E:C9:3D:10', null, null, 0, null, null, null, '', getdate(), 0
UNION ALL
SELECT 4, 3, 22, null, 33, '1001', '13182939966', '20:F8:5E:C9:3D:10', null, null, 0, null, null, null, '', getdate(), 0
UNION ALL
SELECT 5, 3, 22, null, 33, '1002', '13182939966', '20:F8:5E:C9:3D:10', null, null, 0, null, null, null, '', getdate(), 0
UNION ALL
SELECT 6, 3, 22, null, 33, '1003', '13182939966', '20:F8:5E:C9:3D:10', null, null, 0, null, null, null, '', getdate(), 0
UNION ALL
SELECT 7, 3, 22, null, 33, '1004', '13182939966', '20:F8:5E:C9:3D:10', null, null, 0, null, null, null, '', getdate(), 0
UNION ALL
SELECT 8, 3, 22, null, 33, '1005', '13182939966', '20:F8:5E:C9:3D:10', null, null, 0, null, null, null, '', getdate(), 0
UNION ALL
SELECT 9, 4, 22, null, 33, '1601', '13182939966', '20:F8:5E:C9:3D:10', null, null, 0, null, null, null, '', getdate(), 0
UNION ALL
SELECT 10, 4, 22, null, 33, '1602', '13182939966', '20:F8:5E:C9:3D:10', null, null, 0, null, null, null, '', getdate(), 0
UNION ALL
SELECT 11, 4, 22, null, 33, '1603', '13182939966', '20:F8:5E:C9:3D:10', null, null, 0, null, null, null, '', getdate(), 0
UNION ALL
SELECT 12, 4, 22, null, 33, '1604', '13182939966', '20:F8:5E:C9:3D:10', null, null, 0, null, null, null, '', getdate(), 0
UNION ALL
SELECT 13, 4, 22, null, 33, '1605', '13182939966', '20:F8:5E:C9:3D:10', null, null, 0, null, null, null, '', getdate(), 0
UNION ALL
SELECT 14, 4, 22, null, 33, '1606', '13182939966', '20:F8:5E:C9:3D:10', null, null, 0, null, null, null, '', getdate(), 0
UNION ALL
SELECT 15, 4, 22, null, 33, '1607', '13182939966', '20:F8:5E:C9:3D:10', null, null, 0, null, null, null, '', getdate(), 0
UNION ALL
SELECT 16, 4, 22, null, 33, '1608', '13182939966', '20:F8:5E:C9:3D:10', null, null, 0, null, null, null, '', getdate(), 0
UNION ALL
SELECT 17, 4, 22, null, 33, '1609', '13182939966', '20:F8:5E:C9:3D:10', null, null, 0, null, null, null, '', getdate(), 0
UNION ALL
SELECT 18, 4, 22, null, 33, '1610', '13182939966', '20:F8:5E:C9:3D:10', null, null, 0, null, null, null, '', getdate(), 0


