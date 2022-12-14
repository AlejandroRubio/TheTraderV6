USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[dividendos_esperados_porcentaje_poraccion]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[dividendos_esperados_porcentaje_poraccion]
as
select accion, 
YEAR(fecha) as año, 
round(avg(importe_inicial)                          , 2 ) as importe_inicial,
round(sum(ganancia_neta)                            , 2 ) as ganancia_neta, 
round(sum(ganancia_neta) *100 /avg(importe_inicial) , 2 ) as mi_rentabilidad,
round(sum(ganancia_neta) *100 /max(numero_acciones*valor_accion_enfecha)   , 2 ) as rentabilidad_enfecha
from dividendos_esperados
group by accion, YEAR(fecha)
GO
