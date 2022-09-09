USE [INFO_BURSATIL]
GO
/****** Object:  Table [dbo].[info_acciones_tipologia]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[info_acciones_tipologia](
	[accion] [nvarchar](255) NULL,
	[sector] [nvarchar](255) NULL,
	[subsector] [varchar](255) NULL,
	[pais] [nvarchar](255) NULL,
	[estrategia] [nvarchar](255) NULL
) ON [PRIMARY]
GO
