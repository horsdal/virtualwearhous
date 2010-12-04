DROP TABLE [dbo].[SupplierProduct]
DROP TABLE [dbo].[Supplier]
DROP TABLE [dbo].[Consumer]
DROP TABLE [dbo].[Contact]
DROP TABLE [dbo].[CountryOffer]
DROP TABLE [dbo].[Country]
DROP TABLE [dbo].[Product]
DROP TABLE [dbo].[Package]
GO

USE [UnicefVirtualWarehouse]
GO

/****** Object:  Table [dbo].[Product]    Script Date: 12/04/2010 18:17:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



/****** Object:  Table [dbo].[Package]    Script Date: 12/04/2010 18:16:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Package](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[MinUnit] [int] NOT NULL,
	[Size] [int] NOT NULL,
	[Price] [int] NOT NULL,
	[Product_ID] [int] NOT NULL,
 CONSTRAINT [PK_Packages] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Country]    Script Date: 12/04/2010 16:15:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Country](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [UnicefVirtualWarehouse]
GO

/****** Object:  Table [dbo].[Contact]    Script Date: 12/04/2010 16:15:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Contact](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Address] [varchar](200) NOT NULL,
	[ZIP] [varchar](50) NOT NULL,
	[Country_ID] [int] NOT NULL,
	[City] [varchar](200) NOT NULL,
	[Fax] [varchar](50) NULL,
	[Email] [varchar](100) NULL,
	[Phone] [varchar](50) NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_Country] FOREIGN KEY([Country_ID])
REFERENCES [dbo].[Country] ([ID])
GO

ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_Country]
GO

USE [UnicefVirtualWarehouse]
GO

/****** Object:  Table [dbo].[Consumer]    Script Date: 12/04/2010 16:13:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Consumer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Contact_ID] [int] NOT NULL,
 CONSTRAINT [PK_Consumer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Consumer]  WITH CHECK ADD  CONSTRAINT [FK_Consumer_Contact] FOREIGN KEY([Contact_ID])
REFERENCES [dbo].[Contact] ([ID])
GO

ALTER TABLE [dbo].[Consumer] CHECK CONSTRAINT [FK_Consumer_Contact]
GO

USE [UnicefVirtualWarehouse]
GO

/****** Object:  Table [dbo].[Supplier]    Script Date: 12/04/2010 16:14:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Supplier](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Contact_ID] [int] NOT NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Supplier]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_Contact] FOREIGN KEY([Contact_ID])
REFERENCES [dbo].[Contact] ([ID])
GO

ALTER TABLE [dbo].[Supplier] CHECK CONSTRAINT [FK_Supplier_Contact]
GO

USE [UnicefVirtualWarehouse]
GO


/****** Object:  Table [dbo].[CountryOffer]    Script Date: 12/04/2010 16:15:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CountryOffer](
	[Country_ID] [int] NOT NULL,
	[Package_ID] [int] NOT NULL,
	[PriceFactor] [float] NULL,
	[AuthorizedForSelling] [bit] NOT NULL,
 CONSTRAINT [PK_CountryOffer] PRIMARY KEY CLUSTERED 
(
	[Country_ID] ASC,
	[Package_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CountryOffer]  WITH CHECK ADD  CONSTRAINT [FK_CountryOffer_Country] FOREIGN KEY([Country_ID])
REFERENCES [dbo].[Country] ([ID])
GO

ALTER TABLE [dbo].[CountryOffer] CHECK CONSTRAINT [FK_CountryOffer_Country]
GO

ALTER TABLE [dbo].[CountryOffer]  WITH CHECK ADD  CONSTRAINT [FK_CountryOffer_Package] FOREIGN KEY([Package_ID])
REFERENCES [dbo].[Package] ([ID])
GO

ALTER TABLE [dbo].[CountryOffer] CHECK CONSTRAINT [FK_CountryOffer_Package]
GO


/****** Object:  Table [dbo].[SupplierProduct]    Script Date: 12/04/2010 16:15:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SupplierProduct](
	[Supplier_ID] [int] NOT NULL,
	[Product_ID] [int] NOT NULL,
	[Licensed] [bit] NOT NULL,
 CONSTRAINT [PK_SupplierProduct] PRIMARY KEY CLUSTERED 
(
	[Supplier_ID] ASC,
	[Product_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SupplierProduct]  WITH CHECK ADD  CONSTRAINT [FK_SupplierProduct_Product] FOREIGN KEY([Product_ID])
REFERENCES [dbo].[Product] ([ID])
GO

ALTER TABLE [dbo].[SupplierProduct] CHECK CONSTRAINT [FK_SupplierProduct_Product]
GO

ALTER TABLE [dbo].[SupplierProduct]  WITH CHECK ADD  CONSTRAINT [FK_SupplierProduct_Supplier] FOREIGN KEY([Supplier_ID])
REFERENCES [dbo].[Supplier] ([ID])
GO

ALTER TABLE [dbo].[SupplierProduct] CHECK CONSTRAINT [FK_SupplierProduct_Supplier]
GO

USE [UnicefVirtualWarehouse]
GO
