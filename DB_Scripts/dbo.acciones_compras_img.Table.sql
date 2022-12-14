USE [INFO_BURSATIL]
GO
/****** Object:  Table [dbo].[acciones_compras_img]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[acciones_compras_img](
	[id] [nvarchar](255) NULL,
	[accion] [nvarchar](255) NULL,
	[fecha] [datetime] NULL,
	[numero_acciones] [int] NULL,
	[valor_accion] [decimal](18, 4) NULL,
	[comision] [decimal](18, 2) NULL,
	[broker] [nvarchar](255) NULL
) ON [PRIMARY]
GO
