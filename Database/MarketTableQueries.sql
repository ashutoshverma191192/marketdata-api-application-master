USE [InventoryLite]
GO


/****** Object:  Table [market].[Applications]    Script Date: 3/21/2020 8:55:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [market].[Applications](
	[ID] [int] IDENTITY(1,1) NOT NULL,	
	[Name] [nvarchar](100) NOT NULL,
	[BillingAmount] [float] NOT NULL,
	[BillingFrequency] [int] NOT NULL,
	[BilledBy] [nvarchar](100) NULL,
	[LastBillPaidOn] [datetime] NULL,
	[Remarks] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 
GO

CREATE TABLE [market].[CityMasters](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_CityMasters] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [market].[Stores](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[AddressLine1] [nvarchar](100) NOT NULL,
	[AddressLine2] [nvarchar](100) NULL,
	[CityID] [int] NOT NULL,
	[PostalCode] [int] NOT NULL,
	[PhoneNumber] [nvarchar](15) NOT NULL,
	[PhoneNumber2] [nvarchar](15) NULL,
	[ContactPerson] [nvarchar](50) NOT NULL,
	[Remark] [nvarchar](100) NULL,
	[ApplicationID] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Stores] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [market].[Stores]  WITH CHECK ADD  CONSTRAINT [FK_market_Stores_Applications] FOREIGN KEY([ApplicationID])
REFERENCES [market].[Applications] ([ID])
GO

ALTER TABLE [market].[Stores] CHECK CONSTRAINT [FK_market_Stores_Applications]
GO

ALTER TABLE [market].[Stores]  WITH CHECK ADD  CONSTRAINT [FK_market_Stores_City] FOREIGN KEY([CityID])
REFERENCES [market].[CityMasters] ([ID])
GO

ALTER TABLE [market].[Stores] CHECK CONSTRAINT [FK_market_Stores_City]
GO

CREATE TABLE [market].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[HashedPassword] [varbinary](1024) NOT NULL,
	[Salt] [varbinary](1024) NOT NULL,
	[IsLocked] [bit] NOT NULL,
	[StoreID] [int] NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [market].[Users]  WITH CHECK ADD  CONSTRAINT [FK_market_Users_Stores] FOREIGN KEY([StoreID])
REFERENCES [market].[Stores] ([ID])
GO

ALTER TABLE [market].[Users] CHECK CONSTRAINT [FK_market_Users_Stores]
GO
ALTER TABLE [market].[Users]  WITH CHECK ADD  CONSTRAINT [FK_market_Users_Applications] FOREIGN KEY([ApplicationID])
REFERENCES [market].[Applications] ([ID])
GO

ALTER TABLE [market].[Users] CHECK CONSTRAINT [FK_market_Users_Applications]
GO

CREATE TABLE [market].[UserStores](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[StoreId] [int] NOT NULL,
 CONSTRAINT [PK_UserStores] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [market].[UserStores]  WITH CHECK ADD  CONSTRAINT [FK_market_UserStore_Stores] FOREIGN KEY([StoreId])
REFERENCES [market].[Stores] ([ID])
GO

ALTER TABLE [market].[UserStores] CHECK CONSTRAINT [FK_market_UserStore_Stores]
GO

ALTER TABLE [market].[UserStores]  WITH CHECK ADD  CONSTRAINT [FK_market_UserStore_Users] FOREIGN KEY([UserId])
REFERENCES [market].[Users] ([ID])
GO

ALTER TABLE [market].[UserStores] CHECK CONSTRAINT [FK_market_UserStore_Users]
GO

CREATE TABLE [market].[Roles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [market].[UserRoles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [market].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_market_UserRole_Roles] FOREIGN KEY([RoleId])
REFERENCES [market].[Roles] ([ID])
GO

ALTER TABLE [market].[UserRoles] CHECK CONSTRAINT [FK_market_UserRole_Roles]
GO

ALTER TABLE [market].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_market_UserRole_Users] FOREIGN KEY([UserId])
REFERENCES [market].[Users] ([ID])
GO

ALTER TABLE [market].[UserRoles] CHECK CONSTRAINT [FK_market_UserRole_Users]
GO

CREATE TABLE [market].[Groups](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedBy] [int] NOT NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [market].[Groups]  WITH CHECK ADD  CONSTRAINT [FK_market_Groups_Applications] FOREIGN KEY([ApplicationID])
REFERENCES [market].[Applications] ([ID])
GO

ALTER TABLE [market].[Groups] CHECK CONSTRAINT [FK_market_Groups_Applications]
GO
ALTER TABLE [market].[Groups]  WITH CHECK ADD  CONSTRAINT [FK_market_Groups_CreatedUsers] FOREIGN KEY([CreatedBy])
REFERENCES [market].[Users] ([ID])
GO

ALTER TABLE [market].[Groups] CHECK CONSTRAINT [FK_market_Groups_CreatedUsers]
GO
ALTER TABLE [market].[Groups]  WITH CHECK ADD  CONSTRAINT [FK_market_Groups_UpdatedUsers] FOREIGN KEY([UpdatedBy])
REFERENCES [market].[Users] ([ID])
GO

ALTER TABLE [market].[Groups] CHECK CONSTRAINT [FK_market_Groups_UpdatedUsers]
GO

CREATE TABLE [market].[SubGroups](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[AccountNumber] [nvarchar](100) NULL,
	[AddressLine1] [nvarchar](100) NULL,
	[AddressLine2] [nvarchar](100) NULL,
	[City] [int] NULL,
	[PostalCode] [int] NULL,
	[PhoneNumber] [nvarchar](100) NULL,
	[OpeningBalance] [float] NULL,
	[ShowInSaleBill] [bit] NOT NULL,
	[IsExpense] [bit] NOT NULL,
	[GroupID] [int] NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedBy] [int] NOT NULL,
 CONSTRAINT [PK_SubGroups] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [market].[SubGroups]  WITH CHECK ADD  CONSTRAINT [FK_market_SubGroupGroup] FOREIGN KEY([GroupID])
REFERENCES [market].[Groups] ([ID])
GO

ALTER TABLE [market].[SubGroups] CHECK CONSTRAINT [FK_market_SubGroupGroup]
GO

ALTER TABLE [market].[SubGroups]  WITH CHECK ADD  CONSTRAINT [FK_market_Subgroups_Applications] FOREIGN KEY([ApplicationID])
REFERENCES [market].[Applications] ([ID])
GO
ALTER TABLE [market].[SubGroups] CHECK CONSTRAINT [FK_market_Subgroups_Applications]
GO
ALTER TABLE [market].[SubGroups]  WITH CHECK ADD  CONSTRAINT [FK_market_SubGroups_CreatedUsers] FOREIGN KEY([CreatedBy])
REFERENCES [market].[Users] ([ID])
GO

ALTER TABLE [market].[SubGroups] CHECK CONSTRAINT [FK_market_SubGroups_CreatedUsers]
GO
ALTER TABLE [market].[SubGroups]  WITH CHECK ADD  CONSTRAINT [FK_market_SubGroups_UpdatedUsers] FOREIGN KEY([UpdatedBy])
REFERENCES [market].[Users] ([ID])
GO

ALTER TABLE [market].[SubGroups] CHECK CONSTRAINT [FK_market_SubGroups_UpdatedUsers]
GO

CREATE TABLE [market].[Items](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[Unit] [int] NOT NULL,
	[IsWeighable] [bit] NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedBy] [int] NOT NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [market].[Items]  WITH CHECK ADD  CONSTRAINT [FK_market_Items_Applications] FOREIGN KEY([ApplicationID])
REFERENCES [market].[Applications] ([ID])
GO
ALTER TABLE [market].[Items] CHECK CONSTRAINT [FK_market_Items_Applications]
GO
ALTER TABLE [market].[Items]  WITH CHECK ADD  CONSTRAINT [FK_market_Items_CreatedUsers] FOREIGN KEY([CreatedBy])
REFERENCES [market].[Users] ([ID])
GO

ALTER TABLE [market].[Items] CHECK CONSTRAINT [FK_market_Items_CreatedUsers]
GO
ALTER TABLE [market].[Items]  WITH CHECK ADD  CONSTRAINT [FK_market_Items_UpdatedUsers] FOREIGN KEY([UpdatedBy])
REFERENCES [market].[Users] ([ID])
GO

ALTER TABLE [market].[Items] CHECK CONSTRAINT [FK_market_Items_UpdatedUsers]
GO

CREATE TABLE [market].[Qualities](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[ItemID] [int] NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedBy] [int] NOT NULL,
 CONSTRAINT [PK_Qualities] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [market].[Qualities]  WITH CHECK ADD  CONSTRAINT [FK_market_Qualities_Applications] FOREIGN KEY([ApplicationID])
REFERENCES [market].[Applications] ([ID])
GO
ALTER TABLE [market].[Qualities] CHECK CONSTRAINT [FK_market_Qualities_Applications]
GO
ALTER TABLE [market].[Qualities]  WITH CHECK ADD  CONSTRAINT [FK_market_Qualities_Items] FOREIGN KEY([ItemID])
REFERENCES [market].[Items] ([ID])
GO
ALTER TABLE [market].[Qualities] CHECK CONSTRAINT [FK_market_Qualities_Items]
GO
ALTER TABLE [market].[Qualities]  WITH CHECK ADD  CONSTRAINT [FK_market_Qualities_CreatedUsers] FOREIGN KEY([CreatedBy])
REFERENCES [market].[Users] ([ID])
GO

ALTER TABLE [market].[Qualities] CHECK CONSTRAINT [FK_market_Qualities_CreatedUsers]
GO
ALTER TABLE [market].[Qualities]  WITH CHECK ADD  CONSTRAINT [FK_market_Qualities_UpdatedUsers] FOREIGN KEY([UpdatedBy])
REFERENCES [market].[Qualities] ([ID])
GO

ALTER TABLE [market].[Qualities] CHECK CONSTRAINT [FK_market_Qualities_UpdatedUsers]
GO
