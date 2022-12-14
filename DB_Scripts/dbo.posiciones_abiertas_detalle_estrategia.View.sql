USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[posiciones_abiertas_detalle_estrategia]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE view [dbo].[posiciones_abiertas_detalle_estrategia]
as
SELECT t.estrategia, 
sum(pa.total_compra) as total_compra,
sum(pa.total_actual) as total_actual
FROM posiciones_abiertas_total_accion pa
left join info_acciones_tipologia t
on pa.accion=t.accion
group by t.estrategia

GO
