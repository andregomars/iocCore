use IO_Online
GO

create table dbo.Core_SMS
(
	ID UNIQUEIDENTIFIER not null Default NEWID(),
	MessageID varchar(50) null,
	SubMessageID varchar(50) null,
	SMSType char(5) null,
	SenderCode varchar(50) null,
	ReceiverCode varchar(2000) null,
	Status char(5) null,
	CreateTime Datetime null,
	SendTime Datetime null,
	Message nvarchar(4000),
	IsDone smallint Default 0
 CONSTRAINT [Core_SMS_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)
) ON [PRIMARY]
