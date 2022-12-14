USE [INFO_BURSATIL]
GO
/****** Object:  Table [dbo].[metales]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[metales](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nvarchar](max) NULL,
	[fecha_compra] [datetime] NULL,
	[total_compra] [decimal](18, 4) NULL,
	[total_actual] [decimal](18, 4) NULL,
	[ganancia_total] [decimal](18, 4) NULL,
	[url] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
