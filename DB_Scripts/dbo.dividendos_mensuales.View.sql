USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[dividendos_mensuales]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Script for SelectTopNRows command from SSMS  ******/
CREATE view [dbo].[dividendos_mensuales]
as
select ano, mes, sum(ganancia_neta) as ganancia_neta
from
(
   SELECT year(fecha) as ano, month(fecha) as mes, sum(ganancia_neta) as ganancia_neta
   FROM [INFO_BURSATIL].[dbo].[dividendos]
   group by year(fecha), month(fecha)

   union
   (
   select 2019, 1,  0 union
   select 2019, 2,  0 union
   select 2019, 3,  0 union
   select 2019, 4,  0 union
   select 2019, 5,  0 union
   select 2019, 6,  0 union
   select 2019, 7,  0 union
   select 2019, 8,  0 union
   select 2019, 9,  0 union
   select 2019, 10, 0 union
   select 2019, 11, 0 union
   select 2019, 12, 0 union
   select 2020, 1,  0 union
   select 2020, 2,  0 union
   select 2020, 3,  0 union
   select 2020, 4,  0 union
   select 2020, 5,  0 union
   select 2020, 6,  0 union
   select 2020, 7,  0 union
   select 2020, 8,  0 union
   select 2020, 9,  0 union
   select 2020, 10, 0 union
   select 2020, 11, 0 union
   select 2020, 12, 0 union
   select 2021, 1,  0 union
   select 2021, 2,  0 union
   select 2021, 3,  0 union
   select 2021, 4,  0 union
   select 2021, 5,  0 union
   select 2021, 6,  0 union
   select 2021, 7,  0 union
   select 2021, 8,  0 union
   select 2021, 9,  0 union
   select 2021, 10, 0 union
   select 2021, 11, 0 union
   select 2021, 12, 0 
   )
) a group by ano, mes
GO
