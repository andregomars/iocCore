use IO_Online
go

insert into Core_SMS
(MessageID
,SubMessageID
,SMSType
,SenderCode
,ReceiverCode
,Status
,CreateTime
,SendTime
,Message)
values 
(null
,null
,1
,'48507075'
,'6262521073,6263334444'
,0
,getdate()
,null
,'test go wild')
