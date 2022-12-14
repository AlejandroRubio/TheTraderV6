USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[rentabilidad_posiciones_abiertas]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[rentabilidad_posiciones_abiertas] as
SELECT accion,
total_compra,
total_actual,
case when total_compra=0 then 1000
else
( (total_actual) - total_compra )* 100 / total_compra
end  as porcentaje,
total_actual - total_compra as total
  FROM [INFO_BURSATIL].[dbo].[posiciones_abiertas]
GO
