USE [IO_Online]
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='HAMS_NetDataItem') 
	DROP TABLE dbo.HAMS_NetDataItem
GO

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
	[CreateTime] [datetime] NULL,
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
--vid: 4370 - 01
SELECT '9A30B6D9-8780-4269-BDAD-1C8A67317A7C', 0, null, null, null, 917, 'Total mileage', 0, 6237.9, null, 'miles', GETDATE()
UNION ALL
SELECT '9A30B6D9-8780-4269-BDAD-1C8A67317A7C', 0, null, null, null, 84, 'Vehicle speed', 0, 35, null, 'mph', GETDATE()
UNION ALL
SELECT '9A30B6D9-8780-4269-BDAD-1C8A67317A7C', 0, null, null, null, 9007, 'Range', 0, 253, null, 'miles', GETDATE()
UNION ALL
SELECT '9A30B6D9-8780-4269-BDAD-1C8A67317A7C', 0, null, null, null, 190, 'Engine speed', 0, 0, null, 'rpm', GETDATE()
UNION ALL
SELECT '9A30B6D9-8780-4269-BDAD-1C8A67317A7C', 0, null, null, null, 9012, 'Engine hour', 0, 317.7, null, 'h', GETDATE()
UNION ALL
SELECT '9A30B6D9-8780-4269-BDAD-1C8A67317A7C', 0, null, null, null, 9013, 'Idel time', 0, 95, null, 'h', GETDATE()
UNION ALL
SELECT '9A30B6D9-8780-4269-BDAD-1C8A67317A7C', 0, null, null, null, 9002, 'Voltage', 0, 220, null, 'V', GETDATE()
UNION ALL
SELECT '9A30B6D9-8780-4269-BDAD-1C8A67317A7C', 0, null, null, null, 9005, 'Engine temp', 0, 65, null, 'F', GETDATE()
UNION ALL
SELECT '9A30B6D9-8780-4269-BDAD-1C8A67317A7C', 0, null, null, null, 9006, 'Coolant temp', 0, 55, null, 'F', GETDATE()
UNION ALL
SELECT '9A30B6D9-8780-4269-BDAD-1C8A67317A7C', 0, null, null, null, 4001, 'SOC', 0, 80, null, 'A', GETDATE()
UNION ALL
SELECT '9A30B6D9-8780-4269-BDAD-1C8A67317A7C', 0, null, null, null, 4002, 'Current', 0, 80, null, 'A', GETDATE()
UNION ALL
SELECT '9A30B6D9-8780-4269-BDAD-1C8A67317A7C', 0, null, null, null, 4003, 'Remaining energy', 0, 12, null, 'kWh', GETDATE()
UNION ALL
SELECT '9A30B6D9-8780-4269-BDAD-1C8A67317A7C', 0, null, null, null, 4004, 'Status', 0, 0, null, '', GETDATE()

--vid: 4370 - 02
UNION ALL
SELECT 'CE6B4F7C-8562-49E5-8829-3FBF05E63B11', 0, null, null, null, 917, 'Total mileage', 0, 6239.8, null, 'miles', GETDATE()
UNION ALL
SELECT 'CE6B4F7C-8562-49E5-8829-3FBF05E63B11', 0, null, null, null, 84, 'Vehicle speed', 0, 40, null, 'mph', GETDATE()
UNION ALL
SELECT 'CE6B4F7C-8562-49E5-8829-3FBF05E63B11', 0, null, null, null, 9007, 'Range', 0, 245, null, 'miles', GETDATE()
UNION ALL
SELECT 'CE6B4F7C-8562-49E5-8829-3FBF05E63B11', 0, null, null, null, 190, 'Engine speed', 0, 0, null, 'rpm', GETDATE()
UNION ALL
SELECT 'CE6B4F7C-8562-49E5-8829-3FBF05E63B11', 0, null, null, null, 9012, 'Engine hour', 0, 317.7, null, 'h', GETDATE()
UNION ALL
SELECT 'CE6B4F7C-8562-49E5-8829-3FBF05E63B11', 0, null, null, null, 9013, 'Idel time', 0, 95, null, 'h', GETDATE()
UNION ALL
SELECT 'CE6B4F7C-8562-49E5-8829-3FBF05E63B11', 0, null, null, null, 9002, 'Voltage', 0, 220, null, 'V', GETDATE()
UNION ALL
SELECT 'CE6B4F7C-8562-49E5-8829-3FBF05E63B11', 0, null, null, null, 9005, 'Engine temp', 0, 65, null, 'F', GETDATE()
UNION ALL
SELECT 'CE6B4F7C-8562-49E5-8829-3FBF05E63B11', 0, null, null, null, 9006, 'Coolant temp', 0, 55, null, 'F', GETDATE()
UNION ALL
SELECT 'CE6B4F7C-8562-49E5-8829-3FBF05E63B11', 0, null, null, null, 4001, 'SOC', 0, 70, null, 'A', GETDATE()
UNION ALL
SELECT 'CE6B4F7C-8562-49E5-8829-3FBF05E63B11', 0, null, null, null, 4002, 'Current', 0, 80, null, 'A', GETDATE()
UNION ALL
SELECT 'CE6B4F7C-8562-49E5-8829-3FBF05E63B11', 0, null, null, null, 4003, 'Remaining energy', 0, 10, null, 'kWh', GETDATE()
UNION ALL
SELECT 'CE6B4F7C-8562-49E5-8829-3FBF05E63B11', 0, null, null, null, 4004, 'Status', 0, 0, null, '', GETDATE()

--vid: 4370 - 03
UNION ALL
SELECT 'ED3EEDCE-3536-4656-AA34-C133F55C0853', 0, null, null, null, 917, 'Total mileage', 0, 6242.1, null, 'miles', GETDATE()
UNION ALL
SELECT 'ED3EEDCE-3536-4656-AA34-C133F55C0853', 0, null, null, null, 84, 'Vehicle speed', 0, 45, null, 'mph', GETDATE()
UNION ALL
SELECT 'ED3EEDCE-3536-4656-AA34-C133F55C0853', 0, null, null, null, 9007, 'Range', 0, 235, null, 'miles', GETDATE()
UNION ALL
SELECT 'ED3EEDCE-3536-4656-AA34-C133F55C0853', 0, null, null, null, 190, 'Engine speed', 0, 0, null, 'rpm', GETDATE()
UNION ALL
SELECT 'ED3EEDCE-3536-4656-AA34-C133F55C0853', 0, null, null, null, 9012, 'Engine hour', 0, 317.7, null, 'h', GETDATE()
UNION ALL
SELECT 'ED3EEDCE-3536-4656-AA34-C133F55C0853', 0, null, null, null, 9013, 'Idel time', 0, 95, null, 'h', GETDATE()
UNION ALL
SELECT 'ED3EEDCE-3536-4656-AA34-C133F55C0853', 0, null, null, null, 9002, 'Voltage', 0, 220, null, 'V', GETDATE()
UNION ALL
SELECT 'ED3EEDCE-3536-4656-AA34-C133F55C0853', 0, null, null, null, 9005, 'Engine temp', 0, 65, null, 'F', GETDATE()
UNION ALL
SELECT 'ED3EEDCE-3536-4656-AA34-C133F55C0853', 0, null, null, null, 9006, 'Coolant temp', 0, 55, null, 'F', GETDATE()
UNION ALL
SELECT 'ED3EEDCE-3536-4656-AA34-C133F55C0853', 0, null, null, null, 4001, 'SOC', 0, 65, null, 'A', GETDATE()
UNION ALL
SELECT 'ED3EEDCE-3536-4656-AA34-C133F55C0853', 0, null, null, null, 4002, 'Current', 0, 80, null, 'A', GETDATE()
UNION ALL
SELECT 'ED3EEDCE-3536-4656-AA34-C133F55C0853', 0, null, null, null, 4003, 'Remaining energy', 0, 8, null, 'kWh', GETDATE()
UNION ALL
SELECT 'ED3EEDCE-3536-4656-AA34-C133F55C0853', 0, null, null, null, 4004, 'Status', 0, 0, null, '', GETDATE()

--vid: 4371
UNION ALL
SELECT 'A05E0915-3EA5-4215-B3FA-101E3925CDC0', 0, null, null, null, 917, 'Total mileage', 0, 60, null, 'miles', GETDATE()
UNION ALL
SELECT 'A05E0915-3EA5-4215-B3FA-101E3925CDC0', 0, null, null, null, 84, 'Vehicle speed', 0, 25, null, 'mph', GETDATE()
UNION ALL
SELECT 'A05E0915-3EA5-4215-B3FA-101E3925CDC0', 0, null, null, null, 9007, 'Range', 0, 130, null, 'miles', GETDATE()
UNION ALL
SELECT 'A05E0915-3EA5-4215-B3FA-101E3925CDC0', 0, null, null, null, 190, 'Engine speed', 0, 0, null, 'rpm', GETDATE()
UNION ALL
SELECT 'A05E0915-3EA5-4215-B3FA-101E3925CDC0', 0, null, null, null, 9012, 'Engine hour', 0, 57.7, null, 'h', GETDATE()
UNION ALL
SELECT 'A05E0915-3EA5-4215-B3FA-101E3925CDC0', 0, null, null, null, 9013, 'Idel time', 0, 95, null, 'h', GETDATE()
UNION ALL
SELECT 'A05E0915-3EA5-4215-B3FA-101E3925CDC0', 0, null, null, null, 9002, 'Voltage', 0, 110, null, 'V', GETDATE()
UNION ALL
SELECT 'A05E0915-3EA5-4215-B3FA-101E3925CDC0', 0, null, null, null, 9005, 'Engine temp', 0, 80, null, 'F', GETDATE()
UNION ALL
SELECT 'A05E0915-3EA5-4215-B3FA-101E3925CDC0', 0, null, null, null, 9006, 'Coolant temp', 0, 35, null, 'F', GETDATE()
UNION ALL
SELECT 'A05E0915-3EA5-4215-B3FA-101E3925CDC0', 0, null, null, null, 4001, 'SOC', 0, 20, null, 'A', GETDATE()
UNION ALL
SELECT 'A05E0915-3EA5-4215-B3FA-101E3925CDC0', 0, null, null, null, 4002, 'Current', 0, 100, null, 'A', GETDATE()
UNION ALL
SELECT 'A05E0915-3EA5-4215-B3FA-101E3925CDC0', 0, null, null, null, 4003, 'Remaining energy', 0, 10, null, 'kWh', GETDATE()
UNION ALL
SELECT 'A05E0915-3EA5-4215-B3FA-101E3925CDC0', 0, null, null, null, 4004, 'Status', 0, 0, null, '', GETDATE()

--vid: UPS
UNION ALL
SELECT '9ED42238-939D-493D-8947-B059E27EC7D7', 0, null, null, null, 917, 'Total mileage', 0, 120, null, 'miles', GETDATE()
UNION ALL
SELECT '9ED42238-939D-493D-8947-B059E27EC7D7', 0, null, null, null, 84, 'Vehicle speed', 0, 45, null, 'mph', GETDATE()
UNION ALL
SELECT '9ED42238-939D-493D-8947-B059E27EC7D7', 0, null, null, null, 9007, 'Range', 0, 60, null, 'miles', GETDATE()
UNION ALL
SELECT '9ED42238-939D-493D-8947-B059E27EC7D7', 0, null, null, null, 190, 'Engine speed', 0, 0, null, 'rpm', GETDATE()
UNION ALL
SELECT '9ED42238-939D-493D-8947-B059E27EC7D7', 0, null, null, null, 9012, 'Engine hour', 0, 57.7, null, 'h', GETDATE()
UNION ALL
SELECT '9ED42238-939D-493D-8947-B059E27EC7D7', 0, null, null, null, 9013, 'Idel time', 0, 95, null, 'h', GETDATE()
UNION ALL
SELECT '9ED42238-939D-493D-8947-B059E27EC7D7', 0, null, null, null, 9002, 'Voltage', 0, 110, null, 'V', GETDATE()
UNION ALL
SELECT '9ED42238-939D-493D-8947-B059E27EC7D7', 0, null, null, null, 9005, 'Engine temp', 0, 80, null, 'F', GETDATE()
UNION ALL
SELECT '9ED42238-939D-493D-8947-B059E27EC7D7', 0, null, null, null, 9006, 'Coolant temp', 0, 35, null, 'F', GETDATE()
UNION ALL
SELECT '9ED42238-939D-493D-8947-B059E27EC7D7', 0, null, null, null, 4001, 'SOC', 0, 40, null, 'A', GETDATE()
UNION ALL
SELECT '9ED42238-939D-493D-8947-B059E27EC7D7', 0, null, null, null, 4002, 'Current', 0, 105, null, 'A', GETDATE()
UNION ALL
SELECT '9ED42238-939D-493D-8947-B059E27EC7D7', 0, null, null, null, 4003, 'Remaining energy', 0, 50, null, 'kWh', GETDATE()
UNION ALL
SELECT '9ED42238-939D-493D-8947-B059E27EC7D7', 0, null, null, null, 4004, 'Status', 0, 0, null, '', GETDATE()

--vid: 1001
UNION ALL
SELECT '80A9C151-ECD6-456F-9BA4-C40665DF3A2B', 0, null, null, null, 917, 'Total mileage', 0, 95, null, 'miles', GETDATE()
UNION ALL
SELECT '80A9C151-ECD6-456F-9BA4-C40665DF3A2B', 0, null, null, null, 84, 'Vehicle speed', 0, 32, null, 'mph', GETDATE()
UNION ALL
SELECT '80A9C151-ECD6-456F-9BA4-C40665DF3A2B', 0, null, null, null, 9007, 'Range', 0, 100, null, 'miles', GETDATE()
UNION ALL
SELECT '80A9C151-ECD6-456F-9BA4-C40665DF3A2B', 0, null, null, null, 190, 'Engine speed', 0, 0, null, 'rpm', GETDATE()
UNION ALL
SELECT '80A9C151-ECD6-456F-9BA4-C40665DF3A2B', 0, null, null, null, 9012, 'Engine hour', 0, 57.7, null, 'h', GETDATE()
UNION ALL
SELECT '80A9C151-ECD6-456F-9BA4-C40665DF3A2B', 0, null, null, null, 9013, 'Idel time', 0, 95, null, 'h', GETDATE()
UNION ALL
SELECT '80A9C151-ECD6-456F-9BA4-C40665DF3A2B', 0, null, null, null, 9002, 'Voltage', 0, 110, null, 'V', GETDATE()
UNION ALL
SELECT '80A9C151-ECD6-456F-9BA4-C40665DF3A2B', 0, null, null, null, 9005, 'Engine temp', 0, 80, null, 'F', GETDATE()
UNION ALL
SELECT '80A9C151-ECD6-456F-9BA4-C40665DF3A2B', 0, null, null, null, 9006, 'Coolant temp', 0, 35, null, 'F', GETDATE()
UNION ALL
SELECT '80A9C151-ECD6-456F-9BA4-C40665DF3A2B', 0, null, null, null, 4001, 'SOC', 0, 18, null, 'A', GETDATE()
UNION ALL
SELECT '80A9C151-ECD6-456F-9BA4-C40665DF3A2B', 0, null, null, null, 4002, 'Current', 0, 105, null, 'A', GETDATE()
UNION ALL
SELECT '80A9C151-ECD6-456F-9BA4-C40665DF3A2B', 0, null, null, null, 4003, 'Remaining energy', 0, 22, null, 'kWh', GETDATE()
UNION ALL
SELECT '80A9C151-ECD6-456F-9BA4-C40665DF3A2B', 0, null, null, null, 4004, 'Status', 0, 0, null, '', GETDATE()

--vid: 1002
UNION ALL
SELECT '7BA4A3ED-305C-4522-98CB-3A1CDBD6FE7F', 0, null, null, null, 917, 'Total mileage', 0, 15, null, 'miles', GETDATE()
UNION ALL
SELECT '7BA4A3ED-305C-4522-98CB-3A1CDBD6FE7F', 0, null, null, null, 84, 'Vehicle speed', 0, 43, null, 'mph', GETDATE()
UNION ALL
SELECT '7BA4A3ED-305C-4522-98CB-3A1CDBD6FE7F', 0, null, null, null, 9007, 'Range', 0, 200, null, 'miles', GETDATE()
UNION ALL
SELECT '7BA4A3ED-305C-4522-98CB-3A1CDBD6FE7F', 0, null, null, null, 190, 'Engine speed', 0, 0, null, 'rpm', GETDATE()
UNION ALL
SELECT '7BA4A3ED-305C-4522-98CB-3A1CDBD6FE7F', 0, null, null, null, 9012, 'Engine hour', 0, 57.7, null, 'h', GETDATE()
UNION ALL
SELECT '7BA4A3ED-305C-4522-98CB-3A1CDBD6FE7F', 0, null, null, null, 9013, 'Idel time', 0, 95, null, 'h', GETDATE()
UNION ALL
SELECT '7BA4A3ED-305C-4522-98CB-3A1CDBD6FE7F', 0, null, null, null, 9002, 'Voltage', 0, 150, null, 'V', GETDATE()
UNION ALL
SELECT '7BA4A3ED-305C-4522-98CB-3A1CDBD6FE7F', 0, null, null, null, 9005, 'Engine temp', 0, 80, null, 'F', GETDATE()
UNION ALL
SELECT '7BA4A3ED-305C-4522-98CB-3A1CDBD6FE7F', 0, null, null, null, 9006, 'Coolant temp', 0, 35, null, 'F', GETDATE()
UNION ALL
SELECT '7BA4A3ED-305C-4522-98CB-3A1CDBD6FE7F', 0, null, null, null, 4001, 'SOC', 0, 80, null, 'A', GETDATE()
UNION ALL
SELECT '7BA4A3ED-305C-4522-98CB-3A1CDBD6FE7F', 0, null, null, null, 4002, 'Current', 0, 110, null, 'A', GETDATE()
UNION ALL
SELECT '7BA4A3ED-305C-4522-98CB-3A1CDBD6FE7F', 0, null, null, null, 4003, 'Remaining energy', 0, 60, null, 'kWh', GETDATE()
UNION ALL
SELECT '7BA4A3ED-305C-4522-98CB-3A1CDBD6FE7F', 0, null, null, null, 4004, 'Status', 0, 1, null, '', GETDATE()

--vid: 1003
UNION ALL
SELECT '696CEDF1-409E-4AA5-871F-57D328007CE3', 0, null, null, null, 917, 'Total mileage', 0, 0, null, 'miles', GETDATE()
UNION ALL
SELECT '696CEDF1-409E-4AA5-871F-57D328007CE3', 0, null, null, null, 84, 'Vehicle speed', 0, 0, null, 'mph', GETDATE()
UNION ALL
SELECT '696CEDF1-409E-4AA5-871F-57D328007CE3', 0, null, null, null, 9007, 'Range', 0, 240, null, 'miles', GETDATE()
UNION ALL
SELECT '696CEDF1-409E-4AA5-871F-57D328007CE3', 0, null, null, null, 190, 'Engine speed', 0, 0, null, 'rpm', GETDATE()
UNION ALL
SELECT '696CEDF1-409E-4AA5-871F-57D328007CE3', 0, null, null, null, 9012, 'Engine hour', 0, 57.7, null, 'h', GETDATE()
UNION ALL
SELECT '696CEDF1-409E-4AA5-871F-57D328007CE3', 0, null, null, null, 9013, 'Idel time', 0, 95, null, 'h', GETDATE()
UNION ALL
SELECT '696CEDF1-409E-4AA5-871F-57D328007CE3', 0, null, null, null, 9002, 'Voltage', 0, 220, null, 'V', GETDATE()
UNION ALL
SELECT '696CEDF1-409E-4AA5-871F-57D328007CE3', 0, null, null, null, 9005, 'Engine temp', 0, 80, null, 'F', GETDATE()
UNION ALL
SELECT '696CEDF1-409E-4AA5-871F-57D328007CE3', 0, null, null, null, 9006, 'Coolant temp', 0, 35, null, 'F', GETDATE()
UNION ALL
SELECT '696CEDF1-409E-4AA5-871F-57D328007CE3', 0, null, null, null, 4001, 'SOC', 0, 100, null, 'A', GETDATE()
UNION ALL
SELECT '696CEDF1-409E-4AA5-871F-57D328007CE3', 0, null, null, null, 4002, 'Current', 0, 50, null, 'A', GETDATE()
UNION ALL
SELECT '696CEDF1-409E-4AA5-871F-57D328007CE3', 0, null, null, null, 4003, 'Remaining energy', 0, 80, null, 'kWh', GETDATE()
UNION ALL
SELECT '696CEDF1-409E-4AA5-871F-57D328007CE3', 0, null, null, null, 4004, 'Status', 0, 1, null, '', GETDATE()

--vid: 1004
UNION ALL
SELECT 'F8D276ED-B87B-4B2A-8A6F-ABCDB7D8ADCB', 0, null, null, null, 917, 'Total mileage', 0, 0, null, 'miles', GETDATE()
UNION ALL
SELECT 'F8D276ED-B87B-4B2A-8A6F-ABCDB7D8ADCB', 0, null, null, null, 84, 'Vehicle speed', 0, 42, null, 'mph', GETDATE()
UNION ALL
SELECT 'F8D276ED-B87B-4B2A-8A6F-ABCDB7D8ADCB', 0, null, null, null, 9007, 'Range', 0, 240, null, 'miles', GETDATE()
UNION ALL
SELECT 'F8D276ED-B87B-4B2A-8A6F-ABCDB7D8ADCB', 0, null, null, null, 190, 'Engine speed', 0, 0, null, 'rpm', GETDATE()
UNION ALL
SELECT 'F8D276ED-B87B-4B2A-8A6F-ABCDB7D8ADCB', 0, null, null, null, 9012, 'Engine hour', 0, 57.7, null, 'h', GETDATE()
UNION ALL
SELECT 'F8D276ED-B87B-4B2A-8A6F-ABCDB7D8ADCB', 0, null, null, null, 9013, 'Idel time', 0, 95, null, 'h', GETDATE()
UNION ALL
SELECT 'F8D276ED-B87B-4B2A-8A6F-ABCDB7D8ADCB', 0, null, null, null, 9002, 'Voltage', 0, 220, null, 'V', GETDATE()
UNION ALL
SELECT 'F8D276ED-B87B-4B2A-8A6F-ABCDB7D8ADCB', 0, null, null, null, 9005, 'Engine temp', 0, 80, null, 'F', GETDATE()
UNION ALL
SELECT 'F8D276ED-B87B-4B2A-8A6F-ABCDB7D8ADCB', 0, null, null, null, 9006, 'Coolant temp', 0, 35, null, 'F', GETDATE()
UNION ALL
SELECT 'F8D276ED-B87B-4B2A-8A6F-ABCDB7D8ADCB', 0, null, null, null, 4001, 'SOC', 0, 32, null, 'A', GETDATE()
UNION ALL
SELECT 'F8D276ED-B87B-4B2A-8A6F-ABCDB7D8ADCB', 0, null, null, null, 4002, 'Current', 0, 50, null, 'A', GETDATE()
UNION ALL
SELECT 'F8D276ED-B87B-4B2A-8A6F-ABCDB7D8ADCB', 0, null, null, null, 4003, 'Remaining energy', 0, 40, null, 'kWh', GETDATE()
UNION ALL
SELECT 'F8D276ED-B87B-4B2A-8A6F-ABCDB7D8ADCB', 0, null, null, null, 4004, 'Status', 0, 0, null, '', GETDATE()

--vid: 1005
UNION ALL
SELECT '45B323C0-4FF4-4C47-A9CD-994BEA21478E', 0, null, null, null, 917, 'Total mileage', 0, 100, null, 'miles', GETDATE()
UNION ALL
SELECT '45B323C0-4FF4-4C47-A9CD-994BEA21478E', 0, null, null, null, 84, 'Vehicle speed', 0, 65, null, 'mph', GETDATE()
UNION ALL
SELECT '45B323C0-4FF4-4C47-A9CD-994BEA21478E', 0, null, null, null, 9007, 'Range', 0, 240, null, 'miles', GETDATE()
UNION ALL
SELECT '45B323C0-4FF4-4C47-A9CD-994BEA21478E', 0, null, null, null, 190, 'Engine speed', 0, 0, null, 'rpm', GETDATE()
UNION ALL
SELECT '45B323C0-4FF4-4C47-A9CD-994BEA21478E', 0, null, null, null, 9012, 'Engine hour', 0, 57.7, null, 'h', GETDATE()
UNION ALL
SELECT '45B323C0-4FF4-4C47-A9CD-994BEA21478E', 0, null, null, null, 9013, 'Idel time', 0, 95, null, 'h', GETDATE()
UNION ALL
SELECT '45B323C0-4FF4-4C47-A9CD-994BEA21478E', 0, null, null, null, 9002, 'Voltage', 0, 220, null, 'V', GETDATE()
UNION ALL
SELECT '45B323C0-4FF4-4C47-A9CD-994BEA21478E', 0, null, null, null, 9005, 'Engine temp', 0, 80, null, 'F', GETDATE()
UNION ALL
SELECT '45B323C0-4FF4-4C47-A9CD-994BEA21478E', 0, null, null, null, 9006, 'Coolant temp', 0, 35, null, 'F', GETDATE()
UNION ALL
SELECT '45B323C0-4FF4-4C47-A9CD-994BEA21478E', 0, null, null, null, 4001, 'SOC', 0, 35, null, 'A', GETDATE()
UNION ALL
SELECT '45B323C0-4FF4-4C47-A9CD-994BEA21478E', 0, null, null, null, 4002, 'Current', 0, 50, null, 'A', GETDATE()
UNION ALL
SELECT '45B323C0-4FF4-4C47-A9CD-994BEA21478E', 0, null, null, null, 4003, 'Remaining energy', 0, 35, null, 'kWh', GETDATE()
UNION ALL
SELECT '45B323C0-4FF4-4C47-A9CD-994BEA21478E', 0, null, null, null, 4004, 'Status', 0, 1, null, '', GETDATE()