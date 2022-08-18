using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TheTrader.Modelo.Investing;

namespace TheTrader.Controles.BaseDeDatos
{
    class InvestingDatabaseHelper
    {

        public InvestingDatabaseHelper() { }

        public static void InsertDataUsingSqlBulkCopy(DataTable listaValores)
        {

            string connectionString = BaseDatosBaseServicio.GetDatabaseConnection();
            using (var connection = new SqlConnection(connectionString))
            {

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.KeepIdentity, null))
                {

                    bulkCopy.DestinationTableName = "valores_acciones";

                    //bulkCopy.ColumnMappings.Add("id", "id");
                    bulkCopy.ColumnMappings.Add("accion", "accion");
                    bulkCopy.ColumnMappings.Add("fecha", "fecha");
                    bulkCopy.ColumnMappings.Add("precio_cierre", "precio_cierre");
                    bulkCopy.ColumnMappings.Add("precio_apertura", "precio_apertura");
                    bulkCopy.ColumnMappings.Add("precio_maximo", "precio_maximo");
                    bulkCopy.ColumnMappings.Add("precio_minimo", "precio_minimo");
                    bulkCopy.ColumnMappings.Add("volumen", "volumen");
                    bulkCopy.ColumnMappings.Add("variacion", "variacion");

                    try

                    {
                        connection.Open();
                        // Write from the source to the destination.
                        bulkCopy.WriteToServer(listaValores);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        // Close the SqlDataReader. The SqlBulkCopy
                        // object is automatically closed at the end
                        // of the using block.

                    }
                }

            }

        }

        public static void TruncadoValoresAcciones()
        {
            SqlConnection sqlConnection = new SqlConnection(BaseDatosBaseServicio.GetDatabaseConnection());
            sqlConnection.Open();
            string sql = "truncate table dbo.valores_acciones";
            SqlCommand command = new SqlCommand(sql, sqlConnection);
            command.Connection = sqlConnection;
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }



    }
}
