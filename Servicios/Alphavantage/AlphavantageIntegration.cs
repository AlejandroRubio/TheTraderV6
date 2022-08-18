using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TheTrader.Modelo.AlphaVantage;
using System.Linq;
using ServiceStack;
using ServiceStack.Text;
using TheTrader.Utilidades;
using TheTrader.Configuracion.Constantes;
using System.Globalization;
using TheTrader.Servicios;

namespace TheTrader.Controles.Alphavantage
{
    public class AlphavantageIntegration
    {

        private string API_TOKEN = "20YVOHEU5R127ZKW";

        private string URL = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=#SYMBOL#&apikey=#APIKEY#&datatype=csv";

        public AlphavantageIntegration()
        {

        }


        public void StartCheck()
        {
            List<String> listaSimbolos = new List<string>();

            ConstantesServicio constantes = new ConstantesServicio();
            string rutaHTMLEntrada = constantes.ObtenerSetting("Ruta_SimbolosNasdaq");
            listaSimbolos=GestorFicherosPlanosServicio.LeeLineasFicheroALista(rutaHTMLEntrada);



            foreach (String currentSymbol in listaSimbolos)
            {
                string peticionActual = URL;
                peticionActual = peticionActual.Replace("#SYMBOL#", currentSymbol);
                peticionActual = peticionActual.Replace("#APIKEY#", API_TOKEN);

                string contenidoDescargado = MakeAphaRequest(peticionActual);

                ProcessAlphaCSV(contenidoDescargado, currentSymbol);

            }
        }

        public string MakeAphaRequest(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsStringAsync().Result;
        }


        public void ProcessAlphaCSV(string csv, string simbolo)
        {
            DateTime fechaInicio = new DateTime(2020, 1, 1);
            List<AlphaVantageData> s = csv.FromCsv<List<AlphaVantageData>>();
            List<AlphaVantageData> listaFlitadaANual = s.Where(x => x.Timestamp >= fechaInicio).ToList();

            decimal valorMáximo = listaFlitadaANual.Max(x=>x.High);
            decimal valorMinimo = listaFlitadaANual.Min(x => x.Low);

            decimal  porcentaje = valorMinimo * 100 / valorMáximo;
            porcentaje = 100 - porcentaje;

            

            Console.WriteLine(simbolo+": "+
                porcentaje.ToString("C3", CultureInfo.CurrentCulture)+"% "
                +
                valorMinimo.ToString("C3", CultureInfo.CurrentCulture) + " - "+
                valorMáximo.ToString("C3", CultureInfo.CurrentCulture) 
                );



        }


    }
}
