USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[desglose_evolucion_cartera]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[desglose_evolucion_cartera]
as
SELECT  
ACCION,  TOTAL_ACTUAL-[TOTAL_COMPRA] as resultado
FROM [INFO_BURSATIL].[DBO].[POSICIONES_ABIERTAS]
GO
