USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[dashboard_acciones_img]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create view [dbo].[dashboard_acciones_img]
as
select 
accion,
sum(total_cerrado)   as total_cerrado,
sum(total_abierto)   as total_abierto,
sum(total_dividendo) as total_dividendo,
sum(total_cerrado) + sum(total_abierto) + sum(total_dividendo) as total

from 
(

select 
accion, 
sum(total_beneficio_limpio) as total_cerrado,
0 as total_abierto,
0 as total_dividendo
from posiciones_cerradas_img
group by accion

union

select accion,
0 as total_cerrado,
sum(total_actual-total_compra) as total_abierto,
0 as total_dividendo
from  posiciones_abiertas_img
group by accion

 union

select accion, 
0 as total_Cerrado,
0 as total_abierto,
sum(ganancia_neta) as total_dividendo
from dividendos_img
group by accion

) A group by accion 
--order by total desc
GO
