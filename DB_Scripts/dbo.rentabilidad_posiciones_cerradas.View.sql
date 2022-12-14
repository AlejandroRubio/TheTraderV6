USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[rentabilidad_posiciones_cerradas]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[rentabilidad_posiciones_cerradas] 
as
select fecha_venta, accion, total_compra, total_venta, total_beneficio_limpio, total_beneficio_limpio*100/total_compra as rentabilidad
from [posiciones_cerradas]
GO
