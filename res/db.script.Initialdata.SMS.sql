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
,'tel:+16262521073'
,0
,getdate()
,null
,'test go wild')
