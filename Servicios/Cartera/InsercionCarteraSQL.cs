using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TheTrader.Configuracion;
using TheTrader.Controles.BaseDeDatos;
using TheTrader.Modelo;

namespace TheTrader.Controles.Cartera
{
    class InsercionCarteraSQL
    {

        public static void TruncadoTablaAccionesEnCartera()
        {
            SqlConnection sqlConnection = new SqlConnection(BaseDatosBaseServicio.GetDatabaseConnection());
            sqlConnection.Open();
            string sql = "truncate table dbo.posiciones_abiertas";

            bool checkImg = ConfiguracionAplicacion.ObtenerBoolGenerica("PROCESA_IMG");
            if (checkImg)
            {
                sql += "_IMG";
            }

            SqlCommand command = new SqlCommand(sql, sqlConnection);
            command.Connection = sqlConnection;
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public static void TruncadoTablaAccionesCerradas()
        {
            SqlConnection sqlConnection = new SqlConnection(BaseDatosBaseServicio.GetDatabaseConnection());
            sqlConnection.Open();
            string sql = "truncate table dbo.posiciones_cerradas";

            bool checkImg = ConfiguracionAplicacion.ObtenerBoolGenerica("PROCESA_IMG");
            if (checkImg)
            {
                sql += "_IMG";
            }
            SqlCommand command = new SqlCommand(sql, sqlConnection);
            command.Connection = sqlConnection;
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }


        public static void InsertaBDAccionesEnCartera(List<AccionEnCartera> listadoAcciones)
        {
            SqlConnection sqlConnection = new SqlConnection(BaseDatosBaseServicio.GetDatabaseConnection());
            sqlConnection.Open();

            foreach (AccionEnCartera accion in listadoAcciones)
            {


                string insert = "INSERT INTO dbo.posiciones_abiertas" +
                "           (id" +
                "           ,accion" +
                "           ,numero_acciones" +
                "           ,fecha_compra" +
                "           ,valor_compra" +
                "           ,comision_compra" +
                "           ,total_compra" +
                "           ,fecha_actual" +
                "           ,valor_actual" +
                "           ,total_actual" +
                "           ,ultima_variacion )" +
                "     VALUES" +
                "	        (@id" +
                "           ,@accion" +
                "           ,@numero_acciones" +

                "           ,@fecha_compra" +
                "           ,@valor_compra" +
                "           ,@comision_compra" +
                "           ,@total_compra" +

                "           ,@fecha_actual" +
                "           ,@valor_actual" +
                "           ,@total_actual"+
                "           ,@ultima_variacion)";



                bool checkImg = ConfiguracionAplicacion.ObtenerBoolGenerica("PROCESA_IMG");
                if (checkImg)
                {
                    insert = insert.Replace("posiciones_abiertas", "posiciones_abiertas_img");      
                }



                SqlCommand command = new SqlCommand(insert, sqlConnection);
                command.Connection = sqlConnection;


                command.Parameters.Add("@id", SqlDbType.NVarChar).Value = accion.IdOperacion;
                command.Parameters.Add("@accion", SqlDbType.NVarChar).Value = accion.Nombre;
                command.Parameters.Add("@numero_acciones", SqlDbType.Int).Value = accion.NumeroAccionesRestantes;

                command.Parameters.Add("@fecha_compra", SqlDbType.DateTime).Value = accion.FechaCompra;
                command.Parameters.Add("@valor_compra", SqlDbType.Decimal).Value = accion.ValorCompra;
                command.Parameters.Add("@comision_compra", SqlDbType.Decimal).Value = accion.ComisionCompra;
                command.Parameters.Add("@total_compra", SqlDbType.Decimal).Value = accion.ValorTotalCompra;

                command.Parameters.Add("@fecha_actual", SqlDbType.DateTime).Value = DateTime.Now;
                command.Parameters.Add("@valor_actual", SqlDbType.Decimal).Value = accion.ValorActual;
                command.Parameters.Add("@total_actual", SqlDbType.Decimal).Value = accion.ValorTotalActual; ;

                command.Parameters.Add("@ultima_variacion", SqlDbType.Decimal).Value = accion.UltimaVariacion; ;

                command.ExecuteNonQuery();


            }


            sqlConnection.Close();
        }


        public static void InsertaBDAccioneCerradas(List<AccionEnCartera> listadoAcciones)
        {
            SqlConnection sqlConnection = new SqlConnection(BaseDatosBaseServicio.GetDatabaseConnection());
            sqlConnection.Open();

            foreach (AccionEnCartera accion in listadoAcciones)
            {


                string insert = "INSERT INTO posiciones_cerradas" +
                "           (id" +
                "           ,accion" +
                "           ,numero_acciones" +
                "           ,fecha_compra" +
                "           ,valor_compra" +
                "           ,comision_compra" +
                "           ,total_compra" +
                "           ,fecha_venta" +
                "           ,valor_venta" +
                "           ,comision_venta" +
                "           ,total_venta" +
                "           ,total_beneficio_limpio" +
                "           ,total_beneficio_sucio)" +
                "     VALUES" +
                "           (@id" +
                "           ,@accion" +
                "           ,@numero_acciones" +
                "           ,@fecha_compra" +
                "           ,@valor_compra" +
                "           ,@comision_compra" +
                "           ,@total_compra" +
                "           ,@fecha_venta" +
                "           ,@valor_venta" +
                "           ,@comision_venta" +
                "           ,@total_venta" +
                "           ,@total_beneficio_limpio" +
                "           ,@total_beneficio_sucio" +
                "		   )";


                bool checkImg = ConfiguracionAplicacion.ObtenerBoolGenerica("PROCESA_IMG");
                if (checkImg)
                {
                    insert = insert.Replace("posiciones_cerradas", "posiciones_cerradas_img");
                }

                SqlCommand command = new SqlCommand(insert, sqlConnection);
                command.Connection = sqlConnection;


                command.Parameters.Add("@id", SqlDbType.NVarChar).Value = accion.IdOperacion;
                command.Parameters.Add("@accion", SqlDbType.NVarChar).Value = accion.Nombre;
                command.Parameters.Add("@numero_acciones", SqlDbType.Int).Value = accion.NumeroAccionesVendidas;

                command.Parameters.Add("@fecha_compra", SqlDbType.DateTime).Value = accion.FechaCompra;
                command.Parameters.Add("@valor_compra", SqlDbType.Decimal).Value = accion.ValorCompra;
                command.Parameters.Add("@comision_compra", SqlDbType.Decimal).Value = accion.ComisionCompra;
                command.Parameters.Add("@total_compra", SqlDbType.Decimal).Value = accion.ValorTotalCompra;

                command.Parameters.Add("@fecha_venta", SqlDbType.DateTime).Value = accion.FechaVenta;
                command.Parameters.Add("@valor_venta", SqlDbType.Decimal).Value = accion.ValorVenta;
                command.Parameters.Add("@comision_venta", SqlDbType.Decimal).Value = accion.ComisionVenta;
                command.Parameters.Add("@total_venta", SqlDbType.Decimal).Value = accion.ValorTotalVenta;

                command.Parameters.Add("@total_beneficio_limpio", SqlDbType.Decimal).Value = accion.BeneficioTotalLimpio;
                command.Parameters.Add("@total_beneficio_sucio", SqlDbType.Decimal).Value = accion.BeneficioTotalSucio;

                command.ExecuteNonQuery();


            }


            sqlConnection.Close();
        }


    }
}
