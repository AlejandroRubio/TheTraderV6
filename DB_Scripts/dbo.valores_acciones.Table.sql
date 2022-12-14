USE [INFO_BURSATIL]
GO
/****** Object:  Table [dbo].[valores_acciones]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[valores_acciones](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[accion] [nvarchar](255) NULL,
	[fecha] [datetime] NULL,
	[precio_cierre] [decimal](18, 2) NULL,
	[precio_apertura] [decimal](18, 2) NULL,
	[precio_maximo] [decimal](18, 2) NULL,
	[precio_minimo] [decimal](18, 2) NULL,
	[volumen] [decimal](18, 2) NULL,
	[variacion] [decimal](18, 2) NULL
) ON [PRIMARY]
GO
