USE [IO_Online]
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='IO_DayTotal') 
	DROP TABLE dbo.IO_DayTotal
GO


CREATE TABLE [dbo].[IO_DayTotal](
	[YYMMDD] [varchar](6) NOT NULL,
	[VehicleId] [int] NOT NULL,
	[DayHours] [smallint] NULL DEFAULT ((0)),
	[Mileage] [float] NULL DEFAULT ((0)),
	[SOC_Charged] [float] NULL DEFAULT ((0)),
	[SOC_Used] [float] NULL DEFAULT ((0)),
	[kWh_Charged] [float] NULL DEFAULT ((0)),
	[kWh_Used] [float] NULL DEFAULT ((0)),
	[Hight_Voltage] [float] NULL DEFAULT ((0)),
	[Low_Voltage] [float] NULL DEFAULT ((0)),
	[Hight_Tmp] [float] NULL DEFAULT ((0)),
	[Low_Tmp] [float] NULL DEFAULT ((0)),
	[Hight_Current] [float] NULL DEFAULT ((0)),
	[Low_Current] [float] NULL DEFAULT ((0)),
	[DataTime] [datetime] NULL,
	[RealTime] [datetime] NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [IO_DayTotal_PK] PRIMARY KEY CLUSTERED 
(
	[YYMMDD] ASC, [VehicleId] ASC
)
) ON [PRIMARY]
GO



INSERT INTO dbo.[IO_DayTotal]
(YYMMDD
,VehicleId
,DayHours
,Mileage
,SOC_Charged
,SOC_Used
,kWh_Charged
,kWh_Used
,Hight_Voltage
,Low_Voltage
,Hight_Tmp
,Low_Tmp
,Hight_Current
,Low_Current
,DataTime
,RealTime
,CreateTime)
SELECT '170503', '26',null, 80, 100, 85, 24, 20,null,null,null,null,null,null,getdate(),getdate(),getdate()
UNION ALL
SELECT '170504', '26',null, 75, 80, 70, 18, 16,null,null,null,null,null,null,getdate(),getdate(),getdate()
UNION ALL
SELECT '170505', '26',null, 50, 33, 22, 8, 7,null,null,null,null,null,null,getdate(),getdate(),getdate()
UNION ALL
SELECT '170506', '26',null, 60, 50, 45, 12, 10,null,null,null,null,null,null,getdate(),getdate(),getdate()
UNION ALL
SELECT '170507', '26',null, 30, 45, 40, 10, 8,null,null,null,null,null,null,getdate(),getdate(),getdate()
UNION ALL
SELECT '170508', '26',null, 12, 30, 27, 6, 5,null,null,null,null,null,null,getdate(),getdate(),getdate()
UNION ALL
SELECT '170509', '26',null, 77, 85, 85, 19, 19,null,null,null,null,null,null,getdate(),getdate(),getdate()