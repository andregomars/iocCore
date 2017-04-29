USE [IO_Online]
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='IO_Fleet') 
	DROP TABLE dbo.IO_Fleet
GO


CREATE TABLE [dbo].[IO_Fleet](
	[FleetID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[UserName] [varchar](20) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[VehicleType] [varchar](20) NOT NULL,
	[IntervalSMS] [int] NULL CONSTRAINT [DF_IO_Fleet_Interval]  DEFAULT ((5)),
	[IntervalNet] [int] NULL CONSTRAINT [DF_IO_Fleet_IntervalNet]  DEFAULT ((5)),
	[Timezone] [int] NULL CONSTRAINT [DF__IO_Fleet__Timezo__5EDF0F2E]  DEFAULT ((0)),
	[TimeOffset] [int] NULL CONSTRAINT [DF_IO_Fleet_TimeOffset]  DEFAULT ((0)),
	[LogFormat] [int] NULL CONSTRAINT [DF__IO_Fleet__LogFor__5FD33367]  DEFAULT ((0)),
	[LogStartTime] [varchar](8) NULL,
	[LogEndTime] [varchar](8) NULL,
	[Remark] [varchar](200) NULL,
	[CreateTime] [datetime] NOT NULL,
	[Status] [int] NULL,
 CONSTRAINT [IO_Fleet_PK] PRIMARY KEY CLUSTERED 
(
	[FleetID] ASC
)
) ON [PRIMARY]
GO

set identity_insert dbo.IO_Fleet on

insert into dbo.IO_Fleet
(FleetID
,CompanyId
,UserName
,Name
,VehicleType
,IntervalSMS
,IntervalNet
,Timezone
,TimeOffset
,LogFormat
,LogStartTime
,LogEndTime
,Remark
,CreateTime
,Status)
select 1, 22, 'AVTA_admin', 'AVTA', 'Bus', 5, 5, -8, 0, 0, null, null, '', getdate(), 0
UNION ALL
select 2, 22, 'BYDUPS_admin', 'BYDUPS', 'Bus', 5, 5, -8, 0, 0, null, null, '', getdate(), 0
UNION ALL
select 3, 22, 'LACMTA_admin', 'LACMTA', 'Bus', 5, 5, -8, 0, 0, null, null, '', getdate(), 0
UNION ALL
select 4, 22, 'LBT_admin', 'LBT', 'Bus', 5, 5, -8, 0, 0, null, null, '', getdate(), 0

