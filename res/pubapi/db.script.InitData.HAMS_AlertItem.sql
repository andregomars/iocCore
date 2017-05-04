USE [IO_Online]
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='HAMS_AlertItem') 
	DROP TABLE dbo.HAMS_AlertItem
GO

CREATE TABLE [dbo].[HAMS_AlertItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DataId] [uniqueidentifier] NOT NULL,
	[ItemCode] [varchar](2) NOT NULL,
	[ItemName] [varchar](300) NULL,
	[Data] [bigint] NULL,
	[Value] [float] NOT NULL,
	[Unit] [varchar](20) NULL,
	[IsSource] [smallint] NULL,
	[Remark] [varchar](20) NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [HAMS_AlertItem_PK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
) ON [PRIMARY]
GO

INSERT INTO [dbo].[HAMS_AlertItem] 
(DataId
,ItemCode
,ItemName
,Data
,Value
,Unit
,IsSource
,Remark
,CreateTime)
SELECT 'B7B4AD60-250A-46F6-9491-6D124DE88192', '1f', 'ABS Malfunction', 0, 0, null, 0, null, getdate()
UNION ALL
SELECT '92F8CBCF-18D2-419F-AED6-0076D56F5D1E', '1c', 'Power Battery Malfunction', 0, 0, null, 0, null, getdate()
UNION ALL
SELECT 'F16EA8CD-BE4C-4119-BC1D-0087A08F9D6D', '1d', 'Battery Leaking', 0, 0, null, 0, null, getdate()
UNION ALL
SELECT '4C526E15-BF9A-4F54-B6D5-00B5A1039D9A', '1f', 'ABS Malfunction', 0, 0, null, 0, null, getdate()
UNION ALL
SELECT 'B514AD81-0BD4-479F-9F1E-00D6EFA07DE1', '1f', 'ABS Malfunction', 0, 0, null, 0, null, getdate()
UNION ALL
SELECT '531D3332-9FC0-4567-95BF-010C4FE9497E', '1d', 'Battery Leaking', 0, 0, null, 0, null, getdate()
UNION ALL
SELECT '7B5EC265-318A-41C8-9358-012D158D40EC', '1f', 'ABS Malfunction', 0, 0, null, 0, null, getdate()
UNION ALL
SELECT 'C4A416F4-905A-4A53-843D-0141E00B1E22', '1c', 'Power Battery Malfunction', 0, 0, null, 0, null, getdate()
UNION ALL
SELECT 'AF106B4C-BABA-4E21-BB6A-016431374616', '1c', 'Power Battery Malfunction', 0, 0, null, 0, null, getdate()
UNION ALL
SELECT '256FB282-1DED-4FDB-855F-0167D94853C9', '1c', 'Power Battery Malfunction', 0, 0, null, 0, null, getdate()
UNION ALL
SELECT 'FCB74332-5CB9-4511-8B6C-017A7923BE21', '1f', 'ABS Malfunction', 0, 0, null, 0, null, getdate()
UNION ALL
SELECT 'F55C9E95-B99B-49A6-8AE2-018C09F16C18', '1d', 'Battery Leaking', 0, 0, null, 0, null, getdate()
UNION ALL
SELECT 'AA88A707-036F-43F5-9C3D-14ADB54F4DE4', '1f', 'ABS Malfunction', 0, 0, null, 0, null, getdate()
UNION ALL
SELECT '91D0E86A-F3D7-4FC8-B197-DF6B58E58C2D', '1c', 'Power Battery Malfunction', 0, 0, null, 0, null, getdate()
