USE [INFO_BURSATIL]
GO
/****** Object:  Table [dbo].[posiciones_abiertas_img]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[posiciones_abiertas_img](
	[id] [nvarchar](255) NULL,
	[accion] [nvarchar](255) NULL,
	[numero_acciones] [int] NULL,
	[fecha_compra] [datetime] NULL,
	[valor_compra] [decimal](18, 2) NULL,
	[comision_compra] [decimal](18, 2) NULL,
	[total_compra] [decimal](18, 2) NULL,
	[fecha_actual] [datetime] NULL,
	[valor_actual] [decimal](18, 2) NULL,
	[total_actual] [decimal](18, 2) NULL,
	[ultima_variacion] [decimal](18, 2) NULL
) ON [PRIMARY]
GO
