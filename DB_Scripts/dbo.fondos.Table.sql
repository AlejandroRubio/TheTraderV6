USE [INFO_BURSATIL]
GO
/****** Object:  Table [dbo].[fondos]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fondos](
	[fondo] [nvarchar](max) NULL,
	[fecha_contratacion] [datetime] NULL,
	[numero_participaciones] [decimal](18, 4) NULL,
	[valor_compra] [decimal](18, 4) NULL,
	[valor_actual] [decimal](18, 4) NULL,
	[total_compra] [decimal](18, 4) NULL,
	[total_actual] [decimal](18, 4) NULL,
	[ganancia_total] [decimal](18, 4) NULL,
	[gestor] [nvarchar](max) NULL,
	[isin] [nvarchar](max) NULL,
	[url_morningstar] [nvarchar](max) NULL,
	[total_inicio_año] [decimal](18, 4) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
