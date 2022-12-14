USE [INFO_BURSATIL]
GO
/****** Object:  Table [dbo].[posiciones_abiertas_20211230]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[posiciones_abiertas_20211230](
	[id] [nvarchar](255) NULL,
	[accion] [nvarchar](255) NULL,
	[numero_acciones] [int] NULL,
	[fecha_compra] [datetime] NULL,
	[valor_compra] [decimal](18, 4) NULL,
	[comision_compra] [decimal](18, 4) NULL,
	[total_compra] [decimal](18, 4) NULL,
	[fecha_actual] [datetime] NULL,
	[valor_actual] [decimal](18, 4) NULL,
	[total_actual] [decimal](18, 4) NULL,
	[ultima_variacion] [decimal](18, 4) NULL
) ON [PRIMARY]
GO
