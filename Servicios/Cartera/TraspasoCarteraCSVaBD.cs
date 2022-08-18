using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TheTrader.Controles.BaseDeDatos;
using TheTrader.Modelo;

namespace TheTrader.Controles.Cartera
{
    class TraspasoCarteraCSVaBD
    {

        public TraspasoCarteraCSVaBD() { }

        internal static void InsertaEnBDAccionesEnCartera(List<AccionEnCartera> acciones, bool esCompra)
        {
            SqlConnection sqlConnection = new SqlConnection(BaseDatosBaseServicio.GetDatabaseConnection());
            sqlConnection.Open();

            foreach (AccionEnCartera accion in acciones)
            {

                string insert = "INSERT INTO [dbo].[@tabla]" +
                              " ([id]" +
                              " ,[fecha]" +
                              " ,[accion]" +
                              " ,[numero_acciones]" +
                              " ,[valor_accion]" +
                              " ,[comision] )" +
                              " VALUES" +
                              " (@id," +
                              " @fecha," +
                              " @accion," +
                              " @numero_acciones," +
                              " @valor_accion, " +
                              " @comision" +
                              ")";


                if (esCompra)
                {
                    insert = insert.Replace("@tabla", "acciones_compras");
                }
                else
                {
                    insert = insert.Replace("@tabla", "acciones_ventas");
                }

                SqlCommand command = new SqlCommand(insert, sqlConnection);
                command.Connection = sqlConnection;

                command.Parameters.Add("@id", SqlDbType.NVarChar, 255).Value = accion.IdOperacion;
                command.Parameters.Add("@accion", SqlDbType.NVarChar, 255).Value = accion.Nombre;




                if (esCompra)
                {
                    command.Parameters.Add("@fecha", SqlDbType.DateTime).Value = accion.FechaCompra;
                    command.Parameters.Add("@numero_acciones", SqlDbType.Int).Value = accion.NumeroAccionesCompradas;
                    command.Parameters.Add("@valor_accion", SqlDbType.Decimal).Value = accion.ValorCompra;
                    command.Parameters.Add("@comision", SqlDbType.Decimal).Value = accion.ComisionCompra;

                }
                else
                {
                    command.Parameters.Add("@fecha", SqlDbType.DateTime).Value = accion.FechaVenta;
                    command.Parameters.Add("@numero_acciones", SqlDbType.Int).Value = accion.NumeroAccionesVendidas;
                    command.Parameters.Add("@valor_accion", SqlDbType.Decimal).Value = accion.ValorVenta;
                    command.Parameters.Add("@comision", SqlDbType.Decimal).Value = accion.ComisionVenta;

                }


                command.ExecuteNonQuery();


            }


            sqlConnection.Close();
        }


    }
}
