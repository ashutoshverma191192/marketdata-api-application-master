use InventoryLite
GO

INSERT INTO [market].[Applications]
           ([Name]
           ,[BillingAmount]
           ,[BillingFrequency]
           ,[BilledBy]
           ,[LastBillPaidOn]
           ,[Remarks]
           ,[CreatedDate])
     VALUES
           (
		   'Market',
		   100,
		   1,
		   'Test',
		   GETDATE(),
		   NULL,
		   GETDATE()
		   )
GO
INSERT INTO [market].[CityMasters]
           ([Name])
     VALUES
           ('Gurgaon')
GO

INSERT INTO [market].[Stores]
           ([Name]
           ,[AddressLine1]
           ,[AddressLine2]
           ,[CityID]
           ,[PostalCode]
           ,[PhoneNumber]
           ,[PhoneNumber2]
           ,[ContactPerson]
           ,[Remark]
           ,[ApplicationID]
           ,[CreatedDate]
           ,[UpdatedDate])
     VALUES
           ('GgnStore',
		   'GGN',
		   NULL,
		   1,
		   122105,
		   1234567890,
		   NULL,
		   'Test',
		   NULL,
		   1,
		   GETDATE(),
		   GETDATE())
GO