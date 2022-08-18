using ContaWeb.Servicios.Dashboards.Informes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TheTrader.Controles.BaseDeDatos;
using TheTrader.Modelo;
using TheTrader.Modelo.Accion;

namespace TheTrader.Servicios.Calculadora
{
    public class CalculadoraRentaVariableService
    {


        //DataPoints para los gráficos
        Dictionary<DateTime, double> valoresComprasEnCartera;
        Dictionary<DateTime, double> valoresTasacionCartera;
        Dictionary<DateTime, double> valoresRentabilidadCartera;
        Dictionary<DateTime, double> valoresTotalDividendos;


        //Valores diarios de todas las acciones operadas
        private List<InfoCotizacionModel> informacionCotizaciones;
        private List<AccionUnidadCalculoModel> listadoMisAcciones;


        public CalculadoraRentaVariableService()
        {

            informacionCotizaciones = new List<InfoCotizacionModel>();
            listadoMisAcciones = new List<AccionUnidadCalculoModel>();

            valoresComprasEnCartera = new Dictionary<DateTime, double>();
            valoresTasacionCartera = new Dictionary<DateTime, double>();
            valoresRentabilidadCartera = new Dictionary<DateTime, double>();
            valoresTotalDividendos = new Dictionary<DateTime, double>();

        }

        /// <summary>
        /// MÉTODO ARRANCADOR:
        /// 
        /// </summary>
        public void EjecutarCalculoHistoricoCartera()
        {

            //Obtener cotizaciones diarias de todos los valores operados
            ObtenerValoresCotizacionesSQL();

            //Obtener historico invertidoJ
            RealizaCalculoEvolucionCantidadInvertida();


            TruncadoHistorico();
            InsertaValoresEnBDD();
            ActualizaDatosEnBBDD();
        }

        public static void TruncadoHistorico()
        {
            SqlConnection sqlConnection = new SqlConnection(BaseDatosBaseServicio.GetDatabaseConnection());
            sqlConnection.Open();
            string sql = "truncate table historico_rendimientos";
            SqlCommand command = new SqlCommand(sql, sqlConnection);
            command.Connection = sqlConnection;
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }



        public static void ActualizaDatosEnBBDD()
        {
            SqlConnection sqlConnection = new SqlConnection(BaseDatosBaseServicio.GetDatabaseConnection());
            sqlConnection.Open();
            string sql = "update historico_rendimientos set total_beneficio = total_valorado - total_invertido";
            SqlCommand command = new SqlCommand(sql, sqlConnection);
            command.Connection = sqlConnection;
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }


        private void InsertaValoresEnBDD()
        {

            DataTable valores = new DataTable();
            valores.Columns.Add(new DataColumn("fecha", typeof(DateTime)));
            valores.Columns.Add(new DataColumn("total_invertido", typeof(decimal)));
            valores.Columns.Add(new DataColumn("total_valorado", typeof(decimal)));
            valores.Columns.Add(new DataColumn("total_beneficio", typeof(decimal)));

            foreach (var item in valoresComprasEnCartera)
            {
                DataRow dr = valores.NewRow();
                dr["fecha"] = item.Key;
                dr["total_invertido"] = item.Value;

                double value = 0;
                bool hasValue = valoresTasacionCartera.TryGetValue(item.Key, out value);
                if (hasValue)
                {
                    dr["total_valorado"] = value;
                }
                else
                {
                    throw new Exception("ERROR");
                }


                dr["total_beneficio"] = 0;
                valores.Rows.Add(dr);
            }


            string connectionString = BaseDatosBaseServicio.GetDatabaseConnection();
            using (var connection = new SqlConnection(connectionString))
            {

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.KeepIdentity, null))
                {

                    bulkCopy.DestinationTableName = "historico_rendimientos";

                    //bulkCopy.ColumnMappings.Add("id", "id");

                    bulkCopy.ColumnMappings.Add("fecha", "fecha");
                    bulkCopy.ColumnMappings.Add("total_invertido", "total_invertido");
                    bulkCopy.ColumnMappings.Add("total_valorado", "total_valorado");
                    bulkCopy.ColumnMappings.Add("total_beneficio", "total_beneficio");


                    try

                    {
                        connection.Open();
                        // Write from the source to the destination.
                        bulkCopy.WriteToServer(valores);
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




        private double ObtenerCotizacionEnDia(DateTime dia, string accion)
        {
            double valorEncontrado = 0;

            //recupero el objeto con todos los valores de un ÚNICA acción
            InfoCotizacionModel valorAccionConcreta = informacionCotizaciones.Where(x => x.accion.Equals(accion.ToUpper())).FirstOrDefault();

            if (valorAccionConcreta == null)
            {
                throw new Exception("Falta la accion en el histórico de BD: " + accion);
            }
            else
            {
                //obtengo valor de un día concreto
                valorAccionConcreta.valoresCotizacion.TryGetValue(dia, out valorEncontrado);
            }

            return valorEncontrado;
        }

        /// <summary>
        /// Obtiene el listado de DataPoints con todos los días y el TOTAL INVERTIDO en ese día (valores de compra)
        /// </summary>
        public void RealizaCalculoEvolucionCantidadInvertida()
        {

            //obtenemos la información de todas las acciones operadas
            listadoMisAcciones = ObtenerInformeRentaVariableSQL();

            //listado de datetiem completo cortando por fecha actual
            ApoyoInformes apoyoService = new ApoyoInformes();
            List<DateTime> listadoCompletoDias = apoyoService.ObtieneTodosLosPutosDiasDeUnAño(true);

            double cantidadTasacionEnDiaXAnterior = 0;
            foreach (DateTime dia in listadoCompletoDias)
            {
                double cantidadInvertidadEnDiaX = 0;
                double cantidadTasacionEnDiaX = 0;


                foreach (AccionUnidadCalculoModel accion in listadoMisAcciones)
                {
                    //si las fecha que estoy procesando esta entre la compra y la venta
                    //  -> sumo el total de acciones
                    if (accion.fecha_compra <= dia && (accion.fecha_venta == null || accion.fecha_venta >= dia))
                    {
                        //tengo la accion en cartera ese día y procedo al calculo
                        cantidadInvertidadEnDiaX += (accion.num_acciones_compra * accion.valor_compra);

                        int numeroReintentos = 0;
                        double valorAFecha = IntentaObtenerValor(dia, accion, numeroReintentos);
                        if (valorAFecha == 0)
                        {
                            throw new Exception("La accion " + accion.accion + " no tiene valor el día: " + dia);
                        }

                        cantidadTasacionEnDiaX += accion.num_acciones_compra * valorAFecha;

                    }
                }

                if (cantidadTasacionEnDiaX == 0)
                {
                    cantidadTasacionEnDiaX = cantidadTasacionEnDiaXAnterior;
                }

                valoresComprasEnCartera.Add(dia, cantidadInvertidadEnDiaX);
                valoresTasacionCartera.Add(dia, cantidadTasacionEnDiaX);
                cantidadTasacionEnDiaXAnterior = cantidadTasacionEnDiaX;
            }
        }

        private double IntentaObtenerValor(DateTime dia, AccionUnidadCalculoModel accion, int numeroReintentos)
        {

            if (numeroReintentos > 10) {
                throw new Exception("PROBLEMA accion " + accion.accion + " no tiene valor el día: " + dia);
            }


            double valorAFecha = ObtenerCotizacionEnDia(dia, accion.accion);

            //reintento para fin de semana
            if (valorAFecha == 0)
            {

                if (dia.DayOfWeek == DayOfWeek.Saturday)
                {
                    valorAFecha = ObtenerCotizacionEnDia(dia.AddDays(-1), accion.accion);
                }
                else if (dia.DayOfWeek == DayOfWeek.Sunday)
                {
                    valorAFecha = ObtenerCotizacionEnDia(dia.AddDays(-2), accion.accion);
                }

            }

            if (valorAFecha == 0)
            {
                numeroReintentos=numeroReintentos+1;
                valorAFecha = IntentaObtenerValor(dia.AddDays(-numeroReintentos), accion, numeroReintentos);
            }




            return valorAFecha;
        }

        /// <summary>
        /// Obtiene el listado con los registros de acciones_cuadromando con todas las acciones operadas
        ///     valores compra y venta asi como fechas
        /// </summary>
        /// <returns></returns>
        public List<AccionUnidadCalculoModel> ObtenerInformeRentaVariableSQL()
        {
            List<AccionUnidadCalculoModel> listadoPosiciones = new List<AccionUnidadCalculoModel>();
            SqlConnection sqlConnection = new SqlConnection(BaseDatosBaseServicio.GetDatabaseConnection());
            String query = "select * from acciones_cuadromando";
            SqlCommand command = new SqlCommand(query, sqlConnection);
            command.Connection = sqlConnection;
            sqlConnection.Open();

            using (var reader = command.ExecuteReader())
            {
                var index1 = reader.GetOrdinal("id");
                var index2 = reader.GetOrdinal("accion");
                var index3 = reader.GetOrdinal("fecha_compra");
                var index4 = reader.GetOrdinal("fecha_venta");
                var index5 = reader.GetOrdinal("num_compra");
                var index6 = reader.GetOrdinal("valor_compra");
                var index7 = reader.GetOrdinal("comision_compra");
                var index8 = reader.GetOrdinal("num_venta");
                var index9 = reader.GetOrdinal("valor_venta");
                var index10 = reader.GetOrdinal("comision_venta");

                while (reader.Read())
                {
                    AccionUnidadCalculoModel accion = new AccionUnidadCalculoModel();
                    accion.id = reader.GetString(index1);
                    accion.accion = reader.GetString(index2);
                    accion.fecha_compra = reader.GetDateTime(index3);

                    if (reader.IsDBNull(index4))
                    {
                        accion.fecha_venta = null;
                        accion.num_acciones_venta = null;
                        accion.valor_venta = null;
                        accion.comision_venta = null;
                    }
                    else
                    {
                        accion.fecha_venta = reader.GetDateTime(index4);
                        accion.num_acciones_venta = reader.GetInt32(index8);
                        accion.valor_venta = Double.Parse(reader.GetDecimal(index9).ToString());
                        accion.comision_venta = Double.Parse(reader.GetDecimal(index10).ToString());
                    }


                    accion.num_acciones_compra = reader.GetInt32(index5);
                    accion.valor_compra = Double.Parse(reader.GetDecimal(index6).ToString());
                    accion.comision_compra = Double.Parse(reader.GetDecimal(index7).ToString());

                    listadoPosiciones.Add(accion);
                }


            }


            sqlConnection.Close();

            return listadoPosiciones;
        }



        /// <summary>
        /// Obtiene el listado de strings con todas las acciones operadas (tanto abiertas como cerradas)
        /// </summary>
        /// <returns></returns>
        private List<string> ObtenerListadoAccionesCargadasSQL()
        {
            List<string> listadoAcciones = new List<string>();
            SqlConnection sqlConnection = new SqlConnection(BaseDatosBaseServicio.GetDatabaseConnection());
            String query = "SELECT distinct accion  FROM valores_acciones";
            SqlCommand command = new SqlCommand(query, sqlConnection);
            command.Connection = sqlConnection;
            sqlConnection.Open();
            using (var reader = command.ExecuteReader())
            {
                var index1 = reader.GetOrdinal("accion");
                while (reader.Read())
                {
                    string accion = reader.GetString(index1);
                    listadoAcciones.Add(accion);
                }
            }
            sqlConnection.Close();
            return listadoAcciones;
        }


        public void ObtenerValoresCotizacionesSQL()
        {
            List<string> listadoAcciones = ObtenerListadoAccionesCargadasSQL();
            foreach (string accion in listadoAcciones)
            {
                InfoCotizacionModel info = new InfoCotizacionModel();
                info.accion = accion;

                Dictionary<DateTime, double> valoresCotizacion = new Dictionary<DateTime, double>();

                SqlConnection sqlConnection = new SqlConnection(BaseDatosBaseServicio.GetDatabaseConnection());

                String query = "SELECT fecha, precio_cierre from valores_acciones where accion='@accion'";
                query = query.Replace("@accion", accion);
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Connection = sqlConnection;
                sqlConnection.Open();

                using (var reader = command.ExecuteReader())
                {
                    var index1 = reader.GetOrdinal("fecha");
                    var index2 = reader.GetOrdinal("precio_cierre");

                    while (reader.Read())
                    {
                        DateTime fecha = reader.GetDateTime(index1);
                        double valor = Double.Parse(reader.GetDecimal(index2).ToString());
                        valoresCotizacion.Add(fecha, valor);
                    }
                }
                info.valoresCotizacion = valoresCotizacion;
                informacionCotizaciones.Add(info);
                sqlConnection.Close();
            }

        }


    }
}
