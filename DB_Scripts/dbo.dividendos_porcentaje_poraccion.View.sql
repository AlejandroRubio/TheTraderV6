USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[dividendos_porcentaje_poraccion]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--cálculo de porcentajes sobre valores máximos de compra y actuales
CREATE view [dbo].[dividendos_porcentaje_poraccion]
as


select 
año,
accion,
sum(ganancia_neta)                          as ganancia_neta,
max(total_compra)                           as importe_inicial,
max(total_en_fecha)                         as importe_en_fecha,

case 
	when sum(ganancia_neta)= 0  then 0
	when max(total_compra) = 0  then 100
    else sum(ganancia_neta) *100/max(total_compra)   
end as rentabilidad_respecto_compra,

case 
	when sum(ganancia_neta)  = 0  then 0
	when max(total_en_fecha) = 0  then 100
    else sum(ganancia_neta) *100/max(total_en_fecha)   
end as rentabilidad_respecto_cotizacion

from
(
   -- en la subconsulta solo se sacan los campos de  importe y totales inciales y actuales
   select 
   year(fecha) as año,
   accion,
   valor_accion_enfecha* numero_acciones as total_en_fecha,
   importe_inicial as total_compra,
   ganancia_neta
   --case 
   --	when ganancia_neta = 0  then 0
   --	when importe_inicial=0  then 100
   --    else round(100*ganancia_neta/importe_inicial ,2)
   --end as rentabilidad_respecto_compra,
   --
   --case 
   --	when ganancia_neta = 0  then 0
   --	when (valor_accion_enfecha* numero_acciones)=0  then 100
   --    else round(100*ganancia_neta/(valor_accion_enfecha* numero_acciones) ,2)
   --end as rentabilidad_respecto_valor
   
   from dividendos
) A
group by año, accion








GO
