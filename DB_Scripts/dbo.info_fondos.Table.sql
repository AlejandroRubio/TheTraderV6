USE [INFO_BURSATIL]
GO
/****** Object:  Table [dbo].[info_fondos]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[info_fondos](
	[fondo] [nvarchar](max) NULL,
	[sector] [nvarchar](max) NULL,
	[sector_participacion] [decimal](18, 4) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
