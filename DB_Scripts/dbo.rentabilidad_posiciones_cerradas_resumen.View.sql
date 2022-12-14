USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[rentabilidad_posiciones_cerradas_resumen]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[rentabilidad_posiciones_cerradas_resumen] as 
select 
coalesce(sum(total_compra)                                ,0)  as total_compra, 
coalesce(sum(total_venta)                                 ,0)  as total_venta,
coalesce(sum(total_beneficio_limpio)                      ,0)  as total_rendimiento,
coalesce(sum(total_beneficio_limpio)*100/sum(total_compra),0)  as porcentaje
from [rentabilidad_posiciones_cerradas]
where year(fecha_venta)= year (getdate())
GO
