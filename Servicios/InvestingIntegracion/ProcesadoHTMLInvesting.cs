using System;
using System.Collections.Generic;
using TheTrader.Configuracion.Constantes;
using TheTrader.Modelo;
using TheTrader.Servicios;
using TheTrader.Utilidades;

namespace TheTrader.Controles
{
    class ProcesadoHTMLInvesting
    {

        private string rutaHTMLEntrada;
        private string rutaCSVSalida;


        List<AccionInvesting> accionInvestingLista = new List<AccionInvesting>();

        public ProcesadoHTMLInvesting()
        {
            int mercado = MenuSelecciónMercado();
            ConstantesServicio constantes = new ConstantesServicio();

            switch(mercado){
                case 1:
                    rutaHTMLEntrada = constantes.ObtenerSetting("Ruta_HTML_Espana");
                    rutaCSVSalida = constantes.ObtenerSetting("Ruta_CSV_Espana");
                    break;
                case 2:
                    rutaHTMLEntrada = constantes.ObtenerSetting("Ruta_HTML_Downjons");
                    rutaCSVSalida = constantes.ObtenerSetting("Ruta_CSV_Downjons");
                    break;
                case 3:
                    rutaHTMLEntrada = constantes.ObtenerSetting("Ruta_HTML_Nasdaq");
                    rutaCSVSalida = constantes.ObtenerSetting("Ruta_CSV_Nasdaq");
                    break;
                case 4:
                    rutaHTMLEntrada = constantes.ObtenerSetting("Ruta_HTML_SP500");
                    rutaCSVSalida = constantes.ObtenerSetting("Ruta_CSV_SP500");
                    break;

            }

        }


        private int MenuSelecciónMercado() {


            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("|  1. Mercado español 234                         |");
            Console.WriteLine("|  2. DownJones 30                                |");
            Console.WriteLine("|  3. Nadaq 100                                   |");
            Console.WriteLine("|  4. SP500 500                                   |");
            Console.WriteLine("--------------------------------------------------");

            var result = Console.ReadLine();

            int enteroMenu = 0;
            try
            {
                enteroMenu = Convert.ToInt32(result);
            }
            catch
            {
                enteroMenu = 0;
            }

            return enteroMenu;




        }

        private void ProcesaHTMLGenerico()
        {
            string[] lineasFichero = GestorFicherosPlanosServicio.LeeLineasFichero(rutaHTMLEntrada);
            int primeraLineaConAccion = 19;
            int saltoDeAccionEnAccion = 12;

            List<String> listadoAcciones = new List<String>();

            bool flagPrimeraLineaProcesada = false;
            int contador = 0;
            foreach (string s in lineasFichero)
            {
                contador++;

                if (!flagPrimeraLineaProcesada)
                {
                    if (contador == primeraLineaConAccion)
                    {
                        contador = 0;
                        flagPrimeraLineaProcesada = true;
                        listadoAcciones.Add(s.Trim());
                    }
                }
                else {
                    if (contador == saltoDeAccionEnAccion)
                    {
                        contador = 0;
                        listadoAcciones.Add(s.Trim());
                    }
                }
                
            }


            foreach (string xml in listadoAcciones) {
                if (xml.Length > 10) {
                    int indice1 = 0;
                    int indice2 = 0;
                    int indice3 = 0;

                    string URL = "";
                    string nombre = "";

                    string TEXTO1 = "<a href=\"";
                    string TEXTO2 = " title=";
                    string TEXTO3 = "</a><span data-name=";
                    string TEXTO4 = ">";


                    indice1 = xml.IndexOf(TEXTO1);
                    indice2 = xml.IndexOf(TEXTO2);
                    URL = xml.Substring(indice1 + TEXTO1.Length, indice2 - TEXTO2.Length - indice1 - 3);

                    indice3 = xml.IndexOf(TEXTO3);
                    nombre = xml.Substring(indice2 + TEXTO2.Length + 1, indice3 - indice2 - 8);
                    var splitado = nombre.Split(TEXTO4);
                    nombre = splitado[1];

                    nombre = nombre.ToUpper().Replace(" ","_");
                    AccionInvesting accionI = new AccionInvesting(nombre, URL);
                    accionInvestingLista.Add(accionI);
                }


               
            }


        }

        public void GeneracionCSVAcciones()
        {
            ProcesaHTMLGenerico();

            List<String> contenidoSalida = new List<String>();

            foreach (AccionInvesting accion in accionInvestingLista)
            {
                String contenido = String.Empty;
                contenido = contenido + ImpresionBaseServicio.FormateAStringAAnchoFijoReverse(accion.Nombre, 40);
                contenido = contenido + "; ";
                contenido = contenido + accion.UrlAccionInvesting;
                contenidoSalida.Add(contenido);
            }

            throw new Exception("Se han cargado un total de "+ contenidoSalida.Count+" acciones para la generación del CSV nacional");


            GestorFicherosPlanosServicio.EscribeContenidoAFichero(contenidoSalida , rutaCSVSalida);

            }
    }
}
