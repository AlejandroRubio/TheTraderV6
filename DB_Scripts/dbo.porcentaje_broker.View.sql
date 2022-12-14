USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[porcentaje_broker]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE view [dbo].[porcentaje_broker]
as
SELECT
broker, 
sum(total_actual) as total_invertido,
(sum(total_actual) *100/  (select sum(total_compra) from[posiciones_abiertas]) ) as porcetanje_brokerage
  FROM [dbo].[posiciones_abiertas] pa
  inner join [dbo].[acciones_compras] ac
  on pa.id=ac.id
  group by broker
GO
