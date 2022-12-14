USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[historico_posiciones]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[historico_posiciones]
as
select accion, fecha_compra, null as fecha_venta, numero_acciones, 'ABIERTA' as estado, total_compra, 0 as total_venta from posiciones_abiertas
union
select accion, fecha_compra, fecha_venta, numero_acciones, 'CERRADA' as estado, total_compra, total_venta from posiciones_cerradas
where fecha_venta>'01/01/2020'
GO
