USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[posiciones_abiertas_total_accion]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[posiciones_abiertas_total_accion]
as
SELECT 
accion, 
sum(total_compra) as total_compra, 
sum(total_actual) as total_actual,
SUM(numero_acciones) as numero_acciones
FROM [INFO_BURSATIL].[dbo].[posiciones_abiertas]
group by  accion
GO
