USE [INFO_BURSATIL]
GO
/****** Object:  Table [dbo].[depositos]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[depositos](
	[año] [int] NULL,
	[deposito] [nvarchar](max) NULL,
	[cantidad_aportada] [decimal](18, 4) NULL,
	[rentabilidad] [decimal](18, 4) NULL,
	[rendimiento_esperado] [decimal](18, 4) NULL,
	[meses_remunerados] [int] NULL,
	[fecha_vencimiento] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
