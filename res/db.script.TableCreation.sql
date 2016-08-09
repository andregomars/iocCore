USE [Core]
GO

CREATE TABLE [dbo].[Core_User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NULL,
	[LoginName] [varchar](30) NOT NULL,
	[UserType] [int] NULL,
	[CompanyID] [int] NOT NULL,
	[Gender] [int] NULL,
	[Password] [varchar](100) NULL,
	[Tel] [varchar](20) NULL,
	[Mobile] [varchar](20) NULL,
	[Email] [varchar](60) NULL,
	[HeadImage] [varchar](500) NULL,
	[ValidDate] [datetime] NULL,
	[IsActive] [int] NULL,
	[InDate] [datetime] NULL DEFAULT (getdate()),
	[InUser] [varchar](30) NULL,
	[EditDate] [datetime] NULL,
	[EditUser] [varchar](30) NULL,
	[Status] [int] NULL  DEFAULT ((0)),
 CONSTRAINT [Core_User_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
)


CREATE TABLE [dbo].[Core_Company](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CompanyType] [varchar](20) NULL,
	[Address] [nvarchar](100) NULL,
	[State] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Tel] [varchar](20) NULL,
	[Mobile] [varchar](20) NULL,
	[Relation] [varchar](50) NULL,
	[Description] [nvarchar](500) NULL,
	[IsStop] [int] NULL,
	[InDate] [datetime] NULL DEFAULT (getdate()),
	[InUser] [varchar](30) NULL,
	[EditDate] [datetime] NULL,
	[EditUser] [varchar](30) NULL,
	[Status] [int] NULL DEFAULT (0),
 CONSTRAINT [Core_Company_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) 


CREATE TABLE [dbo].[Core_Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NOT NULL,
	[RoleType] [varchar](20) NOT NULL,
	[RoleDescription] [varchar](100) NULL,
	[InDate] [datetime] NULL DEFAULT (getdate()),
	[InUser] [varchar](30) NULL,
	[EditDate] [datetime] NULL,
	[EditUser] [varchar](30) NULL,
 CONSTRAINT [core_role_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]



CREATE TABLE [dbo].[Core_UserRole](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
	[InDate] [datetime] NULL DEFAULT (getdate()),
	[InUser] [varchar](30) NULL,
	[EditDate] [datetime] NULL,
	[EditUser] [varchar](30) NULL,
 CONSTRAINT [core_UserRole_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]


CREATE TABLE [dbo].[Core_Function](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MenuID] [int] NULL,
	[FunctionName] [nvarchar](100) NOT NULL,
	[FunctionDescription] [nvarchar](500) NULL,
	[FunctionType] [nvarchar](20) NOT NULL,
	[Priority] [int] NULL,
	[InDate] [datetime] NULL DEFAULT (getdate()),
	[InUser] [varchar](30) NULL,
	[EditDate] [datetime] NULL,
	[EditUser] [varchar](30) NULL,
 CONSTRAINT [core_function_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]


CREATE TABLE [dbo].[Core_Permission](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[FunctionID] [int] NOT NULL,
	[IsEnabled] [int] NULL,
	[InDate] [datetime] NULL DEFAULT (getdate()),
	[InUser] [varchar](30) NULL,
	[EditDate] [datetime] NULL,
	[EditUser] [varchar](30) NULL,
 CONSTRAINT [core_permission_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]
