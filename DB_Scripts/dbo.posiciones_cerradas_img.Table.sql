USE [INFO_BURSATIL]
GO
/****** Object:  Table [dbo].[posiciones_cerradas_img]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[posiciones_cerradas_img](
	[id] [nvarchar](255) NULL,
	[accion] [nvarchar](255) NULL,
	[numero_acciones] [int] NULL,
	[fecha_compra] [datetime] NULL,
	[valor_compra] [decimal](18, 4) NULL,
	[comision_compra] [decimal](18, 4) NULL,
	[total_compra] [decimal](18, 4) NULL,
	[fecha_venta] [datetime] NULL,
	[valor_venta] [decimal](18, 4) NULL,
	[comision_venta] [decimal](18, 4) NULL,
	[total_venta] [decimal](18, 4) NULL,
	[total_beneficio_limpio] [decimal](18, 4) NULL,
	[total_beneficio_sucio] [decimal](18, 4) NULL
) ON [PRIMARY]
GO
