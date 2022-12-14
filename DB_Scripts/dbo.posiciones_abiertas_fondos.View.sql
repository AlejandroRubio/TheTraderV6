USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[posiciones_abiertas_fondos]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE view  [dbo].[posiciones_abiertas_fondos]
as
select * ,

total_actual-total_compra ganancia_total,
(100*total_actual/total_compra) - 100 as rentabilidad
from (
  SELECT f.fondo,
  total_compra,
  total_inicio_año,
  coalesce(evolucion_año,1000) as evolucion_año,
  total_compra +evolucion_año  as total_actual,
  url_morningstar
  FROM [INFO_BURSATIL].[dbo].[fondos] f
  inner  join 
  [CONTABILIDAD_2021].dbo.[CONTAWEB_operaciones_fondos] o
  on f.fondo=o.fondo

) F
GO
