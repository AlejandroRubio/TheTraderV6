using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using TheTrader.Configuracion;
using TheTrader.Controles.BaseDeDatos;
using TheTrader.Controles.Cartera.RecuperacionAccionesEnCartera;
using TheTrader.Modelo;

namespace TheTrader.Controles.Cartera
{
    class RecuperacionAccionesBD : IRecuperacionAccionesEnCartera
    {

        public List<AccionEnCartera> CargarAccionesCompradas()
        {

            //String filepath = ConfiguracionAplicacion.ObtenerRutaCSVCompras();
            List<AccionEnCartera> accionesLeidas = new List<AccionEnCartera>();

            SqlConnection sqlConnection = new SqlConnection(BaseDatosBaseServicio.GetDatabaseConnection());
            SqlCommand cmd = new SqlCommand();

            string consulta = "SELECT id" +
                              "  ,accion" +
                              "  ,fecha" +
                              "  ,numero_acciones" +
                              "  ,valor_accion" +
                              "  ,comision" +
                              "  FROM acciones_compras";


            bool checkImg = ConfiguracionAplicacion.ObtenerBoolGenerica("PROCESA_IMG");
            if (checkImg) {
                consulta += "_IMG";
            }


            cmd.CommandText = consulta;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection;
            sqlConnection.Open();

            using (var reader = cmd.ExecuteReader())
            {
                var indexOfColumn1 = reader.GetOrdinal("id");
                var indexOfColumn2 = reader.GetOrdinal("accion");
                var indexOfColumn3 = reader.GetOrdinal("fecha");
                var indexOfColumn4 = reader.GetOrdinal("numero_acciones");
                var indexOfColumn5 = reader.GetOrdinal("valor_accion");
                var indexOfColumn6 = reader.GetOrdinal("comision");

                while (reader.Read())
                {
                    AccionEnCartera accion = new AccionEnCartera();
                    accion.IdOperacion = reader.GetString(indexOfColumn1);
                    accion.Nombre = reader.GetString(indexOfColumn2);
                    accion.FechaCompra = reader.GetDateTime(indexOfColumn3).ToString();
                    accion.NumeroAccionesCompradas = reader.GetInt32(indexOfColumn4);
                    accion.ValorCompra = float.Parse(reader.GetDecimal(indexOfColumn5).ToString());
                    accion.ComisionCompra = float.Parse(reader.GetDecimal(indexOfColumn6).ToString());
                    accion.ProcesaTotal();

                    accionesLeidas.Add(accion);
                }
            }




            return accionesLeidas;
        }

        public List<AccionEnCartera> CargarAccionesVendidas()
        {

            //String filepath = ConfiguracionAplicacion.ObtenerRutaCSVVentas();
            List<AccionEnCartera> accionesVentas = new List<AccionEnCartera>();


            SqlConnection sqlConnection = new SqlConnection(BaseDatosBaseServicio.GetDatabaseConnection());
            SqlCommand cmd = new SqlCommand();

            string consulta = "SELECT id" +
                              "  ,accion" +
                              "  ,fecha" +
                              "  ,numero_acciones" +
                              "  ,valor_accion" +
                              "  ,comision" +
                              "  FROM acciones_ventas";

            bool checkImg = ConfiguracionAplicacion.ObtenerBoolGenerica("PROCESA_IMG");
            if (checkImg)
            {
                consulta += "_IMG";
            }

            cmd.CommandText = consulta;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection;
            sqlConnection.Open();

            using (var reader = cmd.ExecuteReader())
            {
                var indexOfColumn1 = reader.GetOrdinal("id");
                var indexOfColumn2 = reader.GetOrdinal("accion");
                var indexOfColumn3 = reader.GetOrdinal("fecha");
                var indexOfColumn4 = reader.GetOrdinal("numero_acciones");
                var indexOfColumn5 = reader.GetOrdinal("valor_accion");
                var indexOfColumn6 = reader.GetOrdinal("comision");

                while (reader.Read())
                {
                    AccionEnCartera accion = new AccionEnCartera();
                    accion.IdOperacion = reader.GetString(indexOfColumn1);
                    accion.Nombre = reader.GetString(indexOfColumn2);
                    accion.FechaVenta = reader.GetDateTime(indexOfColumn3).ToString();
                    accion.NumeroAccionesVendidas = reader.GetInt32(indexOfColumn4);
                    accion.ValorVenta = float.Parse(reader.GetDecimal(indexOfColumn5).ToString());
                    accion.ComisionVenta = float.Parse(reader.GetDecimal(indexOfColumn6).ToString());


                    accion.ValorTotalVenta = (accion.ValorVenta * accion.NumeroAccionesVendidas) - accion.ComisionVenta;


                    accionesVentas.Add(accion);
                }
            }



            return accionesVentas;
        }


    }
}
