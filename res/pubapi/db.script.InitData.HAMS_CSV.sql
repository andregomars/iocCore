USE [IO_Online]
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='HAMS_CSV') 
	DROP TABLE dbo.HAMS_CSV
GO

CREATE TABLE [dbo].[HAMS_CSV](
	[CSVId] [bigint] IDENTITY(1,1) NOT NULL,
	[VehicleId] [int] NOT NULL,
	[FilePath] [varchar](200) NULL,
	[FileName] [varchar](200) NOT NULL,
	[FileType] [int] NULL,
	[CFGId] [int] NULL,
	[DailyDate] [datetime] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [HAMS_CSV_PK] PRIMARY KEY CLUSTERED 
(
	[CSVId] ASC
)
) ON [PRIMARY]
GO
