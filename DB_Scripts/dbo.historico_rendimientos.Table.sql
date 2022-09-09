USE [INFO_BURSATIL]
GO
/****** Object:  Table [dbo].[historico_rendimientos]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[historico_rendimientos](
	[fecha] [datetime] NULL,
	[total_invertido] [decimal](18, 4) NULL,
	[total_valorado] [decimal](18, 4) NULL,
	[total_beneficio] [decimal](18, 4) NULL
) ON [PRIMARY]
GO
