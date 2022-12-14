USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[posiciones_abiertas_detalle_pais]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[posiciones_abiertas_detalle_pais]
as
SELECT t.pais, sum(pa.total_actual) as total_actual
  FROM posiciones_abiertas_total_accion pa
  inner join info_acciones_tipologia t
  on pa.accion=t.accion
  group by t.pais
GO
