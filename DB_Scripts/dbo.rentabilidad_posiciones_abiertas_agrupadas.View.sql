USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[rentabilidad_posiciones_abiertas_agrupadas]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view  [dbo].[rentabilidad_posiciones_abiertas_agrupadas]
as
select accion, SUM(total_compra) as total_compra, SUM(total_actual) as total_actual,
SUM(total_actual)-SUM(total_compra) as beneficio,
case when SUM(total_compra) =0 then 100
else ((SUM(total_actual)-SUM(total_compra))*100)/SUM(total_compra)
end as porcentaje
from [rentabilidad_posiciones_abiertas]
group by accion
GO
