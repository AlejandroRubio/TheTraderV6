USE [INFO_BURSATIL]
GO
/****** Object:  Table [dbo].[criptomonedas2]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[criptomonedas2](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[criptomoneda] [nvarchar](255) NULL,
	[token] [nvarchar](255) NULL,
	[fecha_compra] [datetime] NULL,
	[importe_compra] [decimal](18, 4) NULL,
	[comision] [decimal](18, 4) NULL,
	[cantidad_moneda] [decimal](18, 4) NULL,
	[tipo_cambio_compra] [decimal](18, 4) NULL,
	[cotizacion_actual] [decimal](18, 4) NULL,
	[url_investing] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
