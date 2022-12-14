USE [INFO_BURSATIL]
GO
/****** Object:  Table [dbo].[fondos_compras]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fondos_compras](
	[id] [nvarchar](255) NULL,
	[fondo] [nvarchar](255) NULL,
	[fecha] [datetime] NULL,
	[numero_participaciones] [decimal](18, 4) NULL,
	[valor_participacion] [decimal](18, 4) NULL,
	[broker] [nvarchar](255) NULL
) ON [PRIMARY]
GO
