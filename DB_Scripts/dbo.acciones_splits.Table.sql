USE [INFO_BURSATIL]
GO
/****** Object:  Table [dbo].[acciones_splits]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[acciones_splits](
	[accion] [nvarchar](255) NULL,
	[fecha] [datetime] NULL,
	[valor_split] [decimal](18, 2) NULL,
	[valor_previo] [decimal](18, 2) NULL,
	[valor_posterior] [decimal](18, 2) NULL
) ON [PRIMARY]
GO
