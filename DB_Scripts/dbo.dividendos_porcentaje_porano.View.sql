USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[dividendos_porcentaje_porano]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[dividendos_porcentaje_porano] 
as
select 
año,
sum(importe_inicial) as  importe_inicial,
sum(ganancia_neta) as ganancia_neta,
round(sum(ganancia_neta)*100 / sum(importe_inicial),2) as porcentaje_anual
FROM [dbo].[dividendos_porcentaje_poraccion]
group by año
GO
