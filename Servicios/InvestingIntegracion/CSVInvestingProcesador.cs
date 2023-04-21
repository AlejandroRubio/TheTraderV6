using ServiceStack;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using TheTrader.Configuracion;
using TheTrader.Controles.BaseDeDatos;
using TheTrader.Modelo.Investing;

namespace TheTrader.Controles.InvestingIntegracion
{
    class CSVInvestingProcesador
    {

        public CSVInvestingProcesador() { }


        public void EjecutaCargaFicheroInvesting()
        {

            DataTable listaValores = ProcesaFicheroCSVInvesting();
            InvestingDatabaseHelper.TruncadoValoresAcciones();
            InvestingDatabaseHelper.InsertDataUsingSqlBulkCopy(listaValores);

        }

        public DataTable ProcesaFicheroCSVInvesting()
        {

            //Console.Write("Ruta del fichero:");
            //string rutaFicheroCSV = Console.ReadLine();

            string rutaFicheroCSV = ConfiguracionAplicacion.ObtenerRutaGenerica("Ruta_DatosHistoricosInvesting");
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("accion", typeof(String)));
            tbl.Columns.Add(new DataColumn("fecha", typeof(DateTime)));
            tbl.Columns.Add(new DataColumn("precio_cierre", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("precio_apertura", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("precio_maximo", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("precio_minimo", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("volumen", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("variacion", typeof(decimal)));


            string[] fileEntries = Directory.GetFiles(rutaFicheroCSV);

            //bucle por cada fichero leido de la carpeta
            foreach (string fileName in fileEntries)
            {
                Console.WriteLine("Procesando: "+fileName);

                String contenidoFichero = LectorCSVServicio.GenericFileCSVReadString(fileName, true);

                //contenidoFichero = contenidoFichero.Replace("\",\"", ";"); //elimino comillas
                //contenidoFichero = contenidoFichero.Replace("\"", ""); //elimino comillas
                //contenidoFichero = contenidoFichero.Replace(",", "."); //sustiuyo el punto para la fecha
                //contenidoFichero = contenidoFichero.Replace(";", ","); //sustiuyo el punto para la fecha

                
                contenidoFichero = contenidoFichero.Replace("%", ""); //sustiuyo el punto para la fecha
                contenidoFichero = contenidoFichero.Replace("M", ""); //sustiuyo el punto para la fecha
                contenidoFichero = "fecha,precio_ultimo,precio_apertura,precio_maximo,precio_minimo,volumen,variacion\n" + contenidoFichero;

                List<RAW_ValorInvestingCSV> listaRAW = contenidoFichero.FromCsv<List<RAW_ValorInvestingCSV>>();
                List<ValorInvestingCSV> listaValores = new List<ValorInvestingCSV>();




                foreach (RAW_ValorInvestingCSV raw in listaRAW)
                {

                    string sigla= fileName.Replace(rutaFicheroCSV + "\\Datos históricos ", "");
                    sigla = sigla.Replace(".csv","");
                    ValorInvestingCSV valor = new ValorInvestingCSV(MapeoSiglasAccionEnCSV(sigla));
                    valor.ConvertRAW(raw);
                    //listaValores.Add(valor);


                    DataRow dr = tbl.NewRow();
                    // dr["id"] = valor.id;
                    dr["accion"] = valor.accion;
                    dr["fecha"] = valor.fecha;
                    dr["precio_cierre"] = valor.precio_ultimo;
                    dr["precio_apertura"] = valor.precio_apertura;
                    dr["precio_maximo"] = valor.precio_maximo;
                    dr["precio_minimo"] = valor.precio_minimo;
                    dr["volumen"] = valor.volumen;
                    dr["variacion"] = valor.variacion;


                    tbl.Rows.Add(dr);


                }
            }






            return tbl;
        }


        private static string MapeoSiglasAccionEnCSV(String sigla)
        {
            string nombre = "";

            switch (sigla)
            {
                case "SAN":
                    nombre = "SANTANDER";
                    break;
                case "ACS":
                    nombre = "ACS";
                    break;
                case "BBVA":
                    nombre = "BBVA";
                    break;
                case "MSFT":
                    nombre = "MICROSOFT";
                    break;
                case "AIR":
                    nombre = "AIRBUS";
                    break;
                case "EDRE":
                    nombre = "EDREAMS";
                    break;
                case "NTGY":
                    nombre = "NATURGY";
                    break;
                case "2PP":
                    nombre = "PAYPAL";
                    break;
                case "PYPL":
                    nombre = "PAYPAL";
                    break;
                case "SABE":
                    nombre = "SABADELL";
                    break;
                case "INTC":
                    nombre = "INTEL";
                    break;
                case "DIDA":
                    nombre = "DIA";
                    break;
                case "ROVI":
                    nombre = "ROVI";
                    break;
                case "AAPL":
                    nombre = "APPLE";
                    break;
                case "LOG":
                    nombre = "LOGISTA";
                    break;
                case "SOLPW":
                    nombre = "SOLTEC";
                    break;
                case "AHLAy":
                    nombre = "ALIBABA";
                    break;
                case "IBE":
                    nombre = "IBERDROLA";
                    break;
                case "VIS":
                    nombre = "VISCOFAN";
                    break;
                case "MAP":
                    nombre = "MAPFRE";
                    break;
                case "AIRP":
                    nombre = "AIRLIQUIDE";
                    break;
                case "BABA":
                    nombre = "ALIBABA";
                    break;
                case "COIN":
                    nombre = "COINBASE";
                    break;
                case "ELE":
                    nombre = "ENDESA";
                    break;
                case "ACX":
                    nombre = "ACERINOX";
                    break;
                case "SGREN":
                    nombre = "SIEMENS GAMESA";
                    break;
                case "TSLA":
                    nombre = "TESLA";
                    break;
                case "CLNX":
                    nombre = "CELLNEX TELECOM";
                    break;
                case "PHMR":
                    nombre = "PHARMAMAR";
                    break;
                case "OLEO":
                    nombre = "DEOLEO";
                    break;
                case "FLUI":
                    nombre = "FLUIDRA";
                    break;
                case "ENAG":
                    nombre = "ENAGAS";
                    break;
                case "REP":
                    nombre = "REPSOL";
                    break;
                case "RTA4":
                    nombre = "RENTA_4";
                    break;
                case "ORY":
                    nombre = "ORYZON_GENOMICS";
                    break;
                case "TROW":
                    nombre = "TROWE";
                    break;
                case "VID":
                    nombre = "VIDRALA";
                    break;
                case "AMZN":
                    nombre = "AMAZON";
                    break;
                case "JNJ":
                    nombre = "JOHNSON & JOHNSON";
                    break;
                case "O":
                    nombre = "REALTY INCOME";
                    break;
                case "ITX":
                    nombre = "INDITEX";
                    break;
                case "EUR_USD":
                    nombre = "EUR_USD";
                    break;
                case "BAYGn":
                    nombre = "BAYER"; 
                    break;
                case "FAE":
                    nombre = "FAES";
                    break;
                case "MMM":
                    nombre = "3M";
                    break;
                case "PEP":
                    nombre = "PEPSICO";
                    break;
                case "PRIM":
                    nombre = "PRIM";
                    break;
                case "REDE":
                    nombre = "REDE";
                    break;





                default:
                    throw new Exception("Valor no mapeado de acción: " + sigla);

            }

            return nombre;
        }

    }



}
