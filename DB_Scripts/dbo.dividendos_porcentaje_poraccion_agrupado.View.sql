USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[dividendos_porcentaje_poraccion_agrupado]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE view [dbo].[dividendos_porcentaje_poraccion_agrupado] as 
select accion,
MAX(importe_inicial) as importe_inicial,
SUM(ganancia_neta) as ganancia_neta,  

 case when max(importe_inicial) = 0 then 100
 else
 round(sum(ganancia_neta) *100 /max(importe_inicial) , 2 )
 end  as mi_rentabilidad,


 round(sum(ganancia_neta) *100 /max(numero_acciones*valor_accion_enfecha)   , 2 )
 porcentaje_rentabilidad
from dividendos group by accion
GO
