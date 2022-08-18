
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TheTrader.Configuracion.Constantes;
using TheTrader.Modelo;

namespace TheTrader.Configuracion
{
    class ConfiguracionAplicacion
    {

        static public string ObtenerRutaCSVCompras()
        {
            return ObtenerRutaGenerica("Ruta_CSVMisCompras");
        }

        static public string ObtenerRutaCSVVentas()
        {
            return ObtenerRutaGenerica("Ruta_CSVMisVentas");
        }

        static public string ObtenerRutaCSVInvestingEspana()
        {
            return ObtenerRutaGenerica("Ruta_CSV_Espana");
        }


        static public string ObtenerRutaCSVInvesting()
        {
            return ObtenerRutaGenerica("Ruta_CSVInvesting");
        }

        static public string ObtenerRutaCSVInvestingIBEX()
        {
            return ObtenerRutaGenerica("Ruta_CSVInvesting_IBEX");
        }


        static public bool ObtenerBoolGenerica(String key)
        {
            ConstantesServicio constantes = new ConstantesServicio();
            string variable = constantes.ObtenerSetting(key);

            bool salida = false;
            if (variable.ToLower().Equals("true")) {
                salida = true;
            }

            return salida;
        }

        static public string ObtenerRutaGenerica(String rutaGenerica)
        {

            ConstantesServicio constantes = new ConstantesServicio();
            string path = constantes.ObtenerSetting(rutaGenerica);


            if (File.Exists(path) || Directory.Exists(path))
            {
                return path;
            }
            else
            {
                throw new Exception("Error al cargar el CSV con las URLS de Investing. Ruta configurada: " + path);
            }

        }

        static public string ObtenerTokenAlphavantage()
        {
            string path = "";
            return "";

        }


    }
}
