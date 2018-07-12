Use BMS
ALTER TABLE [Warehouse] DROP CONSTRAINT [FK_Warehouse_User]
GO
ALTER TABLE [Warehouse] DROP CONSTRAINT [FK_Warehouse_Location]
GO
ALTER TABLE [Warehouse] DROP CONSTRAINT [FK__Warehouse__Locat__1273C1CD]
GO
ALTER TABLE [User] DROP CONSTRAINT [FK_User_User]
GO
ALTER TABLE [MovementHistory] DROP CONSTRAINT [FK_MovementHistory_Inventory]
GO
ALTER TABLE [MovementHistory] DROP CONSTRAINT [FK_MovementHistory_BinLocation1]
GO
ALTER TABLE [MovementHistory] DROP CONSTRAINT [FK_MovementHistory_BinLocation]
GO
ALTER TABLE [Location] DROP CONSTRAINT [FK_Location_User]
GO
ALTER TABLE [InventoryLocation] DROP CONSTRAINT [FK_InventoryLocation_User]
GO
ALTER TABLE [InventoryLocation] DROP CONSTRAINT [FK__Inventory__Inven__1DE57479]
GO
ALTER TABLE [InventoryLocation] DROP CONSTRAINT [FK__Inventory__BinLo__1CF15040]
GO
ALTER TABLE [Inventory] DROP CONSTRAINT [FK_Inventory_Warehouse]
GO
ALTER TABLE [Inventory] DROP CONSTRAINT [FK_Inventory_User]
GO
ALTER TABLE [Inventory] DROP CONSTRAINT [FK_Inventory_PartDetails]
GO
ALTER TABLE [BinLocation] DROP CONSTRAINT [FK_BinLocation_User]
GO
ALTER TABLE [BinLocation] DROP CONSTRAINT [FK__BinLocati__Wareh__15502E78]
GO
DROP TABLE [Warehouse]
GO
DROP TABLE [User]
GO
DROP TABLE [PartDetails]
GO
DROP TABLE [MovementHistory]
GO
DROP TABLE [Location]
GO
DROP TABLE [InventoryLocation]
GO
DROP TABLE [Inventory]
GO
DROP TABLE [BinLocation]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BinLocation](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](150) NULL,
	[WarehouseId] [int] NULL,
	[Capacity] [int] NULL,
	[BinType] [int] NULL,
	[CreatedById] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK__BinLocat__3214EC27919E7530] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Inventory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[PartId] [int] NULL,
	[WarehouseId] [int] NULL,
	[QRCode] [nvarchar](20) NULL,
	[TotalQuantity] [int] NOT NULL,
	[CreatedById] [int] NULL,
	[Description] [nvarchar](150) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK__Inventor__3214EC2700B71D08] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [InventoryLocation](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[BinLocationId] [int] NULL,
	[Quantity] [int] NULL,
	[InventoryId] [int] NULL,
	[CreatedById] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK__Inventor__3214EC27D1D92E8B] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Location](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](150) NULL,
	[Deleted] [bit] NULL,
	[CreatedById] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK__Location__3214EC279764720B] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [MovementHistory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[InventoryId] [int] NULL,
	[Quantity] [int] NULL,
	[Direction] [nvarchar](5) NULL,
	[FromLocation] [int] NULL,
	[ToLocation] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedById] [int] NULL,
 CONSTRAINT [PK__Inventor__3214EE2700B71D08] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PartDetails](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[PartNo] [nvarchar](20) NOT NULL,
	[CreatedById] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK__Parts__3214EC27C6D97C85] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Role] [int] NULL,
	[CreatedById] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Warehouse](
	[id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](150) NULL,
	[LocationID] [int] NULL,
	[Deleted] [bit] NULL,
	[CreatedById] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK__Warehous__3214EC270AE737AF] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [BinLocation]  WITH CHECK ADD  CONSTRAINT [FK__BinLocati__Wareh__15502E78] FOREIGN KEY([WarehouseId])
REFERENCES [Warehouse] ([Id])
GO
ALTER TABLE [BinLocation] CHECK CONSTRAINT [FK__BinLocati__Wareh__15502E78]
GO
ALTER TABLE [BinLocation]  WITH CHECK ADD  CONSTRAINT [FK_BinLocation_User] FOREIGN KEY([CreatedById])
REFERENCES [User] ([Id])
GO
ALTER TABLE [BinLocation] CHECK CONSTRAINT [FK_BinLocation_User]
GO
ALTER TABLE [Inventory]  WITH CHECK ADD  CONSTRAINT [FK_Inventory_PartDetails] FOREIGN KEY([PartId])
REFERENCES [PartDetails] ([Id])
GO
ALTER TABLE [Inventory] CHECK CONSTRAINT [FK_Inventory_PartDetails]
GO
ALTER TABLE [Inventory]  WITH CHECK ADD  CONSTRAINT [FK_Inventory_User] FOREIGN KEY([CreatedById])
REFERENCES [User] ([Id])
GO
ALTER TABLE [Inventory] CHECK CONSTRAINT [FK_Inventory_User]
GO
ALTER TABLE [Inventory]  WITH CHECK ADD  CONSTRAINT [FK_Inventory_Warehouse] FOREIGN KEY([WarehouseId])
REFERENCES [Warehouse] ([Id])
GO
ALTER TABLE [Inventory] CHECK CONSTRAINT [FK_Inventory_Warehouse]
GO
ALTER TABLE [InventoryLocation]  WITH CHECK ADD  CONSTRAINT [FK__Inventory__BinLo__1CF15040] FOREIGN KEY([BinLocationId])
REFERENCES [BinLocation] ([Id])
GO
ALTER TABLE [InventoryLocation] CHECK CONSTRAINT [FK__Inventory__BinLo__1CF15040]
GO
ALTER TABLE [InventoryLocation]  WITH CHECK ADD  CONSTRAINT [FK__Inventory__Inven__1DE57479] FOREIGN KEY([InventoryId])
REFERENCES [Inventory] ([Id])
GO
ALTER TABLE [InventoryLocation] CHECK CONSTRAINT [FK__Inventory__Inven__1DE57479]
GO
ALTER TABLE [InventoryLocation]  WITH CHECK ADD  CONSTRAINT [FK_InventoryLocation_User] FOREIGN KEY([CreatedById])
REFERENCES [User] ([Id])
GO
ALTER TABLE [InventoryLocation] CHECK CONSTRAINT [FK_InventoryLocation_User]
GO
ALTER TABLE [Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_User] FOREIGN KEY([CreatedById])
REFERENCES [User] ([Id])
GO
ALTER TABLE [Location] CHECK CONSTRAINT [FK_Location_User]
GO
ALTER TABLE [MovementHistory]  WITH CHECK ADD  CONSTRAINT [FK_MovementHistory_BinLocation] FOREIGN KEY([FromLocation])
REFERENCES [BinLocation] ([Id])
GO
ALTER TABLE [MovementHistory] CHECK CONSTRAINT [FK_MovementHistory_BinLocation]
GO
ALTER TABLE [MovementHistory]  WITH CHECK ADD  CONSTRAINT [FK_MovementHistory_BinLocation1] FOREIGN KEY([ToLocation])
REFERENCES [BinLocation] ([Id])
GO
ALTER TABLE [MovementHistory] CHECK CONSTRAINT [FK_MovementHistory_BinLocation1]
GO
ALTER TABLE [MovementHistory]  WITH CHECK ADD  CONSTRAINT [FK_MovementHistory_Inventory] FOREIGN KEY([InventoryId])
REFERENCES [Inventory] ([Id])
GO
ALTER TABLE [MovementHistory] CHECK CONSTRAINT [FK_MovementHistory_Inventory]
GO
ALTER TABLE [User]  WITH CHECK ADD  CONSTRAINT [FK_User_User] FOREIGN KEY([CreatedById])
REFERENCES [User] ([Id])
GO
ALTER TABLE [User] CHECK CONSTRAINT [FK_User_User]
GO
ALTER TABLE [Warehouse]  WITH CHECK ADD  CONSTRAINT [FK__Warehouse__Locat__1273C1CD] FOREIGN KEY([LocationID])
REFERENCES [Location] ([Id])
GO
ALTER TABLE [Warehouse] CHECK CONSTRAINT [FK__Warehouse__Locat__1273C1CD]
GO
ALTER TABLE [Warehouse]  WITH CHECK ADD  CONSTRAINT [FK_Warehouse_Location] FOREIGN KEY([LocationID])
REFERENCES [Location] ([Id])
GO
ALTER TABLE [Warehouse] CHECK CONSTRAINT [FK_Warehouse_Location]
GO
ALTER TABLE [Warehouse]  WITH CHECK ADD  CONSTRAINT [FK_Warehouse_User] FOREIGN KEY([CreatedById])
REFERENCES [User] ([Id])
GO
ALTER TABLE [Warehouse] CHECK CONSTRAINT [FK_Warehouse_User]
GO
ALTER DATABASE [BMS] SET  READ_WRITE 
GO
