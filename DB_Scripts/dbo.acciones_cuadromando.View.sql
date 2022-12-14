USE [INFO_BURSATIL]
GO
/****** Object:  View [dbo].[acciones_cuadromando]    Script Date: 09/09/2022 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  create view [dbo].[acciones_cuadromando]
  as
  select c.id, c.accion, 
  c.fecha as fecha_compra,
  v.fecha as fecha_venta,
  c.numero_acciones as num_compra,
  c.valor_accion as valor_compra,
  c.comision as comision_compra,
  v.numero_acciones as num_venta,
  v.valor_accion as valor_venta,
  v.comision as comision_venta
  from acciones_compras c
  left join acciones_ventas v
  on c.id=v.id
GO
