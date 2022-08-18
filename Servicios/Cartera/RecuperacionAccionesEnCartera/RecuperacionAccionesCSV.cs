using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TheTrader.Configuracion;
using TheTrader.Controles.Cartera.RecuperacionAccionesEnCartera;
using TheTrader.Modelo;

namespace TheTrader.Controles.Cartera
{
    class RecuperacionAccionesCSV : IRecuperacionAccionesEnCartera
    {

        public  List<AccionEnCartera> CargarAccionesCompradas()
        {

            String filepath = ConfiguracionAplicacion.ObtenerRutaCSVCompras();
            List<AccionEnCartera> accionesLeidas = new List<AccionEnCartera>();

            using (var reader = new StreamReader(filepath))
            {
                while (!reader.EndOfStream)
                {

                    var line = reader.ReadLine();
                    if (line.Contains("#"))
                    {
                    }
                    else
                    {
                        var values = line.Split(';');
                        string accionNombre = values[0].Trim();
                        float precioCompra = float.Parse(values[1].Trim());
                        int accionNumeroAcciones = Int32.Parse(values[2].Trim());
                        float accionComision = float.Parse(values[3].Trim());
                        string accionFechaCompra = values[4].Trim();
                        string idOperacion = values[5].Trim();

                        AccionEnCartera accion = new AccionEnCartera();
                        accion.AccionEnCompra(accionNombre, precioCompra, accionFechaCompra, accionNumeroAcciones, accionComision);
                        accion.ValorTotalCompra = (precioCompra * accionNumeroAcciones) + accionComision;
                        accion.IdOperacion = idOperacion;
                        accion.ProcesaTotal();
                        accionesLeidas.Add(accion);
                    }
                }
            }

            return accionesLeidas;
        }

        public  List<AccionEnCartera> CargarAccionesVendidas()
        {

            String filepath =  ConfiguracionAplicacion.ObtenerRutaCSVVentas();
            List<AccionEnCartera> accionesVentas = new List<AccionEnCartera>();

            using (var reader = new StreamReader(filepath))
            {
                while (!reader.EndOfStream)
                {

                    var line = reader.ReadLine();
                    if (line.Contains("#"))
                    {
                    }
                    else
                    {
                        var values = line.Split(';');
                        string accionNombre = values[0].Trim();
                        float precioVenta = float.Parse(values[1].Trim());
                        int accionNumeroAcciones = Int32.Parse(values[2].Trim());
                        float accionComision = float.Parse(values[3].Trim());
                        string accionFechaVenta = values[4].Trim();
                        string idOperacion = values[5].Trim();

                        AccionEnCartera accion = new AccionEnCartera();
                        accion.AccionEnVenta(accionNombre, precioVenta, accionFechaVenta, accionNumeroAcciones, accionComision);
                        accion.IdOperacion = idOperacion;
                        accion.ValorTotalVenta = (precioVenta * accionNumeroAcciones) + accionComision;
                        accionesVentas.Add(accion);
                    }
                }

            }

            return accionesVentas;
        }


    }
}
