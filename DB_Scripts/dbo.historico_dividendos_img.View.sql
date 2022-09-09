USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[historico_dividendos_img]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create view [dbo].[historico_dividendos_img]
as
select year(fecha) as año, sum(ganancia_neta) as ganancia_neta
from dividendos_img
group by year(fecha)
GO
