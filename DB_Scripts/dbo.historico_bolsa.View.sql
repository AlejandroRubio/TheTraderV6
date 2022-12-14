USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[historico_bolsa]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[historico_bolsa]
AS
select 'Dividendos' as categoria, sum(ganancia_neta) as cantidad from dividendos
union
select 'Posiciones cerradas' as categoria, sum(total_beneficio_limpio)  as cantidad from [dbo].[posiciones_cerradas] 
union
select 'Posiciones abiertas' as categoria, sum(total_actual-total_compra)  as cantidad from [dbo].[posiciones_abiertas] 
GO
