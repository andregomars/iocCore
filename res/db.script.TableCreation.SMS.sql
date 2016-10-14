use IO_Online
GO

create table dbo.Core_SMS
(
	ID int identity(1,1) not null,
	MessageID varchar(50) not null,
	SubMessageID varchar(50) null,
	SMSType char(5) null,
	SenderCode varchar(50) null,
	RecieverCode varchar(50) null,
	Status char(5) null,
	CreateTime Datetime null,
	SendTime Datetime null,
	Message nvarchar(4000)
 CONSTRAINT [Core_SMS_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]
