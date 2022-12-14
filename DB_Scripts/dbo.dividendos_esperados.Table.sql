USE [INFO_BURSATIL]
GO
/****** Object:  Table [dbo].[dividendos_esperados]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[dividendos_esperados](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[accion] [nvarchar](255) NULL,
	[fecha] [datetime] NULL,
	[numero_acciones] [int] NULL,
	[valor_accion_enfecha] [decimal](18, 4) NULL,
	[dividendo_por_accion] [decimal](18, 4) NULL,
	[ganancia_bruta] [decimal](18, 4) NULL,
	[comision_banco] [decimal](18, 4) NULL,
	[retencion] [decimal](18, 4) NULL,
	[comision_banco_iva] [decimal](18, 4) NULL,
	[ganancia_neta] [decimal](18, 4) NULL,
	[importe_inicial] [decimal](18, 4) NULL,
	[tipo_dividendo] [nvarchar](100) NULL,
	[banco] [nvarchar](100) NULL
) ON [PRIMARY]
GO
