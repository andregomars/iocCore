USE [IO_Online]
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='HAMS_AlertData') 
	DROP TABLE dbo.HAMS_AlertData
GO


USE [IO_Online]
GO

CREATE TABLE [dbo].[HAMS_AlertData](
	[DataId] [uniqueidentifier] NOT NULL,
	[LinkId] [uniqueidentifier] NULL,
	[VehicleId] [int] NOT NULL,
	[DataTime] [datetime] NULL,
	[DataArray] [varbinary](1024) NULL,
	[IsView] [smallint] NULL,
	[Viewer] [varchar](20) NULL,
	[ViewTime] [datetime] NULL,
	[RealTime] [datetime] NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [HAMS_AlertData_PK] PRIMARY KEY CLUSTERED 
(
	[DataId] ASC
)
) ON [PRIMARY]
GO



INSERT INTO dbo.HAMS_AlertData
(DataId
,LinkId
,VehicleId
,DataTime
,DataArray
,IsView
,Viewer
,ViewTime
,RealTime
,CreateTime)
SELECT 'B7B4AD60-250A-46F6-9491-6D124DE88192', 'B7B4AD60-250A-46F6-9491-6D124DE88192',1,getdate(), null, 0, null, null,getdate(),getdate()
UNION ALL
SELECT '92F8CBCF-18D2-419F-AED6-0076D56F5D1E', '92F8CBCF-18D2-419F-AED6-0076D56F5D1E',1,getdate(), null, 0, null, null,getdate(),getdate()
UNION ALL
SELECT 'F16EA8CD-BE4C-4119-BC1D-0087A08F9D6D', 'F16EA8CD-BE4C-4119-BC1D-0087A08F9D6D',1,getdate(), null, 0, null, null,getdate(),getdate()
UNION ALL
SELECT '4C526E15-BF9A-4F54-B6D5-00B5A1039D9A', '4C526E15-BF9A-4F54-B6D5-00B5A1039D9A',1,getdate(), null, 0, null, null,getdate(),getdate()
UNION ALL
SELECT 'B514AD81-0BD4-479F-9F1E-00D6EFA07DE1', 'B514AD81-0BD4-479F-9F1E-00D6EFA07DE1',1,getdate(), null, 0, null, null,getdate(),getdate()
UNION ALL
SELECT '531D3332-9FC0-4567-95BF-010C4FE9497E', '531D3332-9FC0-4567-95BF-010C4FE9497E',1,getdate(), null, 0, null, null,getdate(),getdate()
UNION ALL
SELECT '7B5EC265-318A-41C8-9358-012D158D40EC', '7B5EC265-318A-41C8-9358-012D158D40EC',1,getdate(), null, 0, null, null,getdate(),getdate()
UNION ALL
SELECT 'C4A416F4-905A-4A53-843D-0141E00B1E22', 'C4A416F4-905A-4A53-843D-0141E00B1E22',1,getdate(), null, 0, null, null,getdate(),getdate()
UNION ALL
SELECT 'AF106B4C-BABA-4E21-BB6A-016431374616', 'AF106B4C-BABA-4E21-BB6A-016431374616',1,getdate(), null, 0, null, null,getdate(),getdate()
UNION ALL
SELECT '256FB282-1DED-4FDB-855F-0167D94853C9', '256FB282-1DED-4FDB-855F-0167D94853C9',1,getdate(), null, 0, null, null,getdate(),getdate()
UNION ALL
SELECT 'FCB74332-5CB9-4511-8B6C-017A7923BE21', 'FCB74332-5CB9-4511-8B6C-017A7923BE21',1,getdate(), null, 0, null, null,getdate(),getdate()
UNION ALL
SELECT 'F55C9E95-B99B-49A6-8AE2-018C09F16C18', 'F55C9E95-B99B-49A6-8AE2-018C09F16C18',1,getdate(), null, 0, null, null,getdate(),getdate()
UNION ALL
SELECT 'AA88A707-036F-43F5-9C3D-14ADB54F4DE4', 'AA88A707-036F-43F5-9C3D-14ADB54F4DE4',2,getdate(), null, 0, null, null,getdate(),getdate()
UNION ALL
SELECT '91D0E86A-F3D7-4FC8-B197-DF6B58E58C2D', '91D0E86A-F3D7-4FC8-B197-DF6B58E58C2D',2,getdate(), null, 0, null, null,getdate(),getdate()
