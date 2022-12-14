USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[posiciones_abiertas_evolucion]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create view [dbo].[posiciones_abiertas_evolucion]
as
select accion, fecha, sum(total_adia) as total_adia, sum(numero_acciones) as numero_acciones, AVG(precio_cierre) as precio_cierre from posiciones_abiertas_historico
group by accion, fecha
GO
