USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[operaciones_mensuales]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[operaciones_mensuales]
as

select 
month(fecha) as mes,
year(fecha) as año,
sum(numero_acciones* valor_accion) as cantidad,
'Compras' as tipo_operacion
from acciones_compras
group by month(fecha) , year(fecha)


union 

select 
month(fecha) as mes,
year(fecha) as año,
0- sum(numero_acciones* valor_accion) as cantidad,
'Ventas' as tipo_operacion
from acciones_ventas
group by month(fecha) , year(fecha)
GO
