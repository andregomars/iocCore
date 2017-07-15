USE IO_Online
GO

Create Proc UP_HAMS_AddSMSReceivingRequestByVehicle 
@VehicleName nvarchar(50)
as

declare @Phone varchar(50)

select top (1) @Phone = RTrim(Phone) from IO_Vehicle with(nolock)
where BusNo = @VehicleName

If @Phone is null
	Return

Insert into Core_SMS
(ID, MessageID, SubMessageID, SMSType, SenderCode, ReceiverCode, Status, CreateTime, SendTime, Message, IsDone)
Values
(NEWID(),null, null, null, null, @Phone, '0', GETDATE(), null, 'IOC:R', 0)