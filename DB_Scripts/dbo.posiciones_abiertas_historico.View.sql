USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[posiciones_abiertas_historico]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[posiciones_abiertas_historico]
as
select va.accion, va.precio_cierre, va.fecha, pa.numero_acciones, pa.numero_acciones*va.precio_cierre as total_adia
from valores_acciones va
inner join posiciones_abiertas pa
on va.accion=pa.accion
GO
