USE [IO_Online]
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='HAMS_NetData') 
	DROP TABLE dbo.HAMS_NetData
GO


CREATE TABLE [dbo].[HAMS_NetData](
	[DataId] [uniqueidentifier] NOT NULL,
	[VehicleId] [int] NOT NULL,
	[MAC] [varchar](50) NULL,
	[GPS] [smallint] NULL CONSTRAINT [DF__HAMS_NetDat__GPS__5CC1BC92]  DEFAULT ((0)),
	[Lng] [varchar](50) NULL,
	[EW] [char](1) NULL CONSTRAINT [DF__HAMS_NetData__EW__5DB5E0CB]  DEFAULT (NULL),
	[Lat] [varchar](50) NULL,
	[SN] [char](1) NULL CONSTRAINT [DF__HAMS_NetData__SN__5EAA0504]  DEFAULT (NULL),
	[DataTime] [datetime] NULL,
	[DataType] [smallint] NULL CONSTRAINT [DF__HAMS_NetD__DataT__5F9E293D]  DEFAULT ((0)),
	[DataArray] [varbinary](1024) NULL,
	[IsView] [smallint] NULL CONSTRAINT [DF_HAMS_NetData_IsView]  DEFAULT ((0)),
	[RealTime] [datetime] NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [HAMS_NetData_PK] PRIMARY KEY CLUSTERED 
(
	[DataId] ASC
)
) ON [PRIMARY]
GO


INSERT INTO dbo.HAMS_NetData
(DataId
,VehicleId
,MAC
,GPS
,Lng
,EW
,Lat
,SN
,DataTime
,DataType
,DataArray
,IsView
,RealTime
,CreateTime)
SELECT '9A30B6D9-8780-4269-BDAD-1C8A67317A7C', 1,'20:F8:5E:C9:3D:10',0, '-117.556485','W', '34.084480', 'N', GETDATE(), 0, NULL, 0, GETDATE(), GETDATE()
UNION ALL
SELECT 'ED3EEDCE-3536-4656-AA34-C133F55C0853', 1,'20:F8:5E:C9:3D:10',0, '-117.558352','W', '34.083840', 'N', DATEADD(MI, -5, GETDATE()), 0, NULL, 0, DATEADD(MI, -5, GETDATE()), DATEADD(MI, -5, GETDATE())
UNION ALL
SELECT 'CE6B4F7C-8562-49E5-8829-3FBF05E63B11', 1,'20:F8:5E:C9:3D:10',0, '-117.558336','W', '34.080091', 'N', DATEADD(MI, -10, GETDATE()), 0, NULL, 0, DATEADD(MI, -10, GETDATE()), DATEADD(MI, -5, GETDATE())
UNION ALL
SELECT 'A05E0915-3EA5-4215-B3FA-101E3925CDC0', 2,'20:F8:5E:C9:3D:10',0, '-117.575890','W', '34.061072', 'N', GETDATE(), 0, NULL, 0, GETDATE(), GETDATE()
UNION ALL
SELECT '615CEF61-FE5A-4F6F-8995-AB935A826654', 2,'20:F8:5E:C9:3D:10',0, '-117.578046','W', '34.061783', 'N', DATEADD(MI, -5, GETDATE()), 0, NULL, 0, DATEADD(MI, -5, GETDATE()), DATEADD(MI, -5, GETDATE())
UNION ALL
SELECT '0266B4BC-4E7D-4974-B116-09E5E2BA2E0F', 3,'20:F8:5E:C9:3D:10',0, '-73.994646','W', '40.734593', 'N', DATEADD(MI, -10, GETDATE()), 0, NULL, 0, DATEADD(MI, -10, GETDATE()), DATEADD(MI, -5, GETDATE())
UNION ALL
SELECT '9ED42238-939D-493D-8947-B059E27EC7D7', 3,'20:F8:5E:C9:3D:10',0, '-73.996298','W', '40.737137', 'N', GETDATE(), 0, NULL, 0, GETDATE(), GETDATE()
UNION ALL
SELECT '80A9C151-ECD6-456F-9BA4-C40665DF3A2B', 4,'20:F8:5E:C9:3D:10',0, '-118.265505','W', '34.043418', 'N', DATEADD(MI, -5, GETDATE()), 0, NULL, 0, DATEADD(MI, -5, GETDATE()), DATEADD(MI, -5, GETDATE())
UNION ALL
SELECT '7BA4A3ED-305C-4522-98CB-3A1CDBD6FE7F', 5,'20:F8:5E:C9:3D:10',0, '-118.468074','W', '34.072125', 'N', DATEADD(MI, -10, GETDATE()), 0, NULL, 0, DATEADD(MI, -10, GETDATE()), DATEADD(MI, -5, GETDATE())
UNION ALL
SELECT '696CEDF1-409E-4AA5-871F-57D328007CE3', 6,'20:F8:5E:C9:3D:10',0, '-118.448048','W', '34.063670', 'N', GETDATE(), 0, NULL, 0, GETDATE(), GETDATE()
UNION ALL
SELECT 'F8D276ED-B87B-4B2A-8A6F-ABCDB7D8ADCB', 7,'20:F8:5E:C9:3D:10',0, '-118.448058','W', '34.073840', 'N', DATEADD(MI, -5, GETDATE()), 0, NULL, 0, DATEADD(MI, -5, GETDATE()), DATEADD(MI, -5, GETDATE())
UNION ALL
SELECT '45B323C0-4FF4-4C47-A9CD-994BEA21478E', 8,'20:F8:5E:C9:3D:10',0, '-118.448233','W', '34.061092', 'N', DATEADD(MI, -10, GETDATE()), 0, NULL, 0, DATEADD(MI, -10, GETDATE()), DATEADD(MI, -5, GETDATE())