USE [Core]
GO

CREATE TABLE [dbo].[Core_User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](30) NULL,
	[LoginName] [varchar](30) NULL,
	[UserType] [varchar](20) NOT NULL,
	[CompanyID] [int] NOT NULL,
	[IsAdmin] [int] NOT NULL,
	[Password] [varchar](100) NULL,
	[CellPhone] [varchar](20) NULL,
	[WorkPhone] [varchar](20) NULL,
	[Email] [varchar](60) NULL,
	[HeadImage] [varchar](500) NULL,
	[InDate] [datetime] NULL DEFAULT (getdate()),
	[InUser] [varchar](30) NULL,
	[EditDate] [datetime] NULL,
	[EditUser] [varchar](30) NULL,
	[Status] [varchar](20) NULL,
 CONSTRAINT [core_user_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]


CREATE TABLE [dbo].[Core_Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NOT NULL,
	[RoleType] [varchar](20) NOT NULL,
	[RoleDescription] [varchar](100) NULL,
	[InDate] [datetime] NULL DEFAULT (getdate()),
	[InUser] [varchar](30) NULL,
	[EditDate] [datetime] NULL,
	[EditUser] [varchar](30) NULL,
	[Status] [varchar](20) NULL,
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
	[InDate] [datetime] NULL DEFAULT (getdate()),
	[InUser] [varchar](30) NULL,
	[EditDate] [datetime] NULL,
	[EditUser] [varchar](30) NULL,
 CONSTRAINT [core_permission_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]

