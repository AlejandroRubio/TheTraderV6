using System;
using System.Data;
using TheTrader.Configuracion.Constantes;
using System.Data.SqlClient;


namespace TheTrader.Controles.BaseDeDatos
{
    class BaseDatosBaseServicio
    {
   

        //construyo la cadena de conexión para un despacho concreto
        public static String GetDatabaseConnection() {

            ConstantesServicio constantes = new ConstantesServicio();
            string rutaHTMLEntrada = constantes.ObtenerSetting("Ruta_SimbolosNasdaq");

            String datasource = String.Empty;
            string SERVIDOR =  constantes.ObtenerSetting("BD_INSTANCIA");
            string BBDD =  constantes.ObtenerSetting("BD_BD");
            string USER =  constantes.ObtenerSetting("BD_USUARIO");
            string PASS =  constantes.ObtenerSetting("BD_PASS");
            datasource = "Server="+SERVIDOR+";Database=" + BBDD + ";User Id="+ USER + ";Password="+ PASS + ";";
            return datasource;

        }


        public static bool ChequeaConexionABBDD()
        {
            bool hayconexionbbdd = false;
            SqlConnection sqlConnection = new SqlConnection(GetDatabaseConnection());
            SqlCommand cmd = new SqlCommand();
            string s = "";
            string consulta = "SELECT 1 as id";

            cmd.CommandText = consulta;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection;
            sqlConnection.Open();

            using (var reader = cmd.ExecuteReader())
            {
                var indexOfColumn1 = reader.GetOrdinal("id");
                while (reader.Read())
                {
                    s = reader.GetInt32(indexOfColumn1).ToString();
                    //Console.WriteLine(s);
                }
            }

            sqlConnection.Close();

            if (s != null && !s.Equals(""))
            {
                hayconexionbbdd = true;
            }

            return hayconexionbbdd;


        }


    }
}
