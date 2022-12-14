USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[rentabilidad_posiciones_abiertas_resumen]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[rentabilidad_posiciones_abiertas_resumen] as 
select 
sum(total_compra) as total_compra, 
sum(total_actual) as total_actual,
sum(total_actual)-sum(total_compra) as total_rendimiento,
(sum(total_actual)-sum(total_compra))*100/sum(total_compra) as porcentaje
from [rentabilidad_posiciones_abiertas]
GO
