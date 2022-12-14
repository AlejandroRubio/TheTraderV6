USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[detalle_inversiones]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create view  [dbo].[detalle_inversiones]
as

select
'TOTAL Posiciones abiertas' as inversion, 
'Renta variable' as categoria,
GETDATE() as fecha,
sum(total_compra) as total_compra,
sum(total_actual) as total_actual,
sum(total_actual) -sum(total_compra) as total_generado,
((sum(total_actual) -sum(total_compra)) * 100 ) /sum(total_compra) as porcentaje_rentabilidad
from posiciones_abiertas 

union

select 
'Estrategia '+estrategia  as inversion ,
'Renta variable' as categoria,
GETDATE() as fecha,
total_compra,
total_actual,
total_actual - total_compra as total_generado,
((total_actual -total_compra) * 100 ) /total_compra as porcentaje_rentabilidad
from posiciones_abiertas_detalle_estrategia

union

select 
'Dividendos cobrados 2021',
'Renta pasiva' as categoria,
GETDATE() as fecha,
sum(importe_inicial) as total_compra,
sum(importe_inicial) as total_actual,
sum(ganancia_neta) as total_generado,
(sum(ganancia_neta) *100 ) / sum(importe_inicial) as porcentaje_rentabilidad
from [dividendos_porcentaje_poraccion] where año =2021

union



select 
'TOTAL Dividendos perpetuos',
'Renta pasiva' as categoria,
GETDATE() as fecha,
sum(importe_inicial) as total_compra,
sum(importe_inicial) as total_actual,
sum(ganancia_neta) as total_generado,
(sum(ganancia_neta) *100 ) / sum(importe_inicial) as porcentaje_rentabilidad
from [dividendos_porcentaje_poraccion_agrupado] 

union

select 
'Fondos de inversión',
'Renta variable' as categoria,
GETDATE() as fecha,
sum(total_compra) as total_compra,
sum(total_actual) as total_actual,
sum(ganancia_total) as total_generado,
(sum(ganancia_total) *100 ) / sum(total_compra) as porcentaje_rentabilidad
from fondos

union



select 
'Remuneracion cuenta: '+ nombre,
'Renta pasiva' as categoria,
GETDATE() as fecha, 
case when nombre = 'MyInvestor' then 15000  else saldo_actual end as total_compra,
case when nombre = 'MyInvestor' then 15000 else saldo_actual end as total_actual,
total_generado,
case 
when nombre = 'MyInvestor' 
	then (total_generado *100 ) / 15000  
else (total_generado *100 ) / saldo_actual 
end as porcentaje_rentabilidad
from [CONTABILIDAD_2021].[dbo].[foto_cuentas_actual] fca
inner join  
(select 
cuenta,
sum(cantidad) as total_generado
from [CONTABILIDAD_2021].[dbo].[detalle_operaciones]
where categoria ='Remuneración cuentas'
group by cuenta ) A
on a.cuenta=fca.nombre

union

select 
'Wallapop',
'Wallapop',
GETDATE() as fecha, 
sum(importe_compra) as total_compra,
sum(importe_venta)-sum(importe_envio) as total_actual,
sum(importe_beneficio) as total_generado,
(sum(importe_beneficio) *100 ) / sum(importe_compra) as porcentaje_rentabilidad
from [CONTABILIDAD_2021].[dbo].[desglose_ventas]

GO
