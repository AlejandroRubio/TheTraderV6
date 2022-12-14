USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[dashboard_acciones]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[dashboard_acciones]
as
select 
accion,
sum(total_cerrado)   as total_cerrado,
sum(total_abierto)   as total_abierto,
sum(total_dividendo) as total_dividendo,
sum(total_cerrado) + sum(total_abierto) + sum(total_dividendo) as total,
sum(rentabilidad_dividendo) as rentabilidad_dividendo

from 
(

select 
accion, 
sum(total_beneficio_limpio) as total_cerrado,
0 as total_abierto,
0 as total_dividendo,
0 as rentabilidad_dividendo
from posiciones_cerradas
group by accion

union

select accion,
0 as total_cerrado,
sum(total_actual-total_compra) as total_abierto,
0 as total_dividendo,
0 as rentabilidad_dividendo
from  posiciones_abiertas
group by accion

 union

select accion, 
0 as total_Cerrado,
0 as total_abierto,
ganancia_neta as total_dividendo,
mi_rentabilidad as rentabilidad_dividendo
from dividendos_porcentaje_poraccion_agrupado



) A group by accion 
--order by total desc
GO
