USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[dividendos_porcentajes_rentabilidad]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[dividendos_porcentajes_rentabilidad]
as
select fecha, accion, ganancia_neta, importe_inicial, valor_accion_enfecha*numero_acciones as importe_presente,
case when importe_inicial <> 0 then
ganancia_neta*100/importe_inicial
else 
ganancia_neta*100/(valor_accion_enfecha*numero_acciones)
end
as rentabilidad_vs_inicio,
ganancia_neta*100/(valor_accion_enfecha*numero_acciones) as rentabilidad_vs_presente
from dividendos
GO
