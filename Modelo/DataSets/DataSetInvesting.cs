using System;
using System.Collections.Generic;
using System.Text;
using TheTrader.Configuracion;
using TheTrader.Controles;
using TheTrader.Controles.InvestingIntegracion;
using TheTrader.Controles.Web;
using TheTrader.Modelo.Scrapping;
using TheTrader.Servicios.Scrapping;
using TheTrader.Utilidades;

namespace TheTrader.Modelo
{
    public class DataSetInvesting
    {

        List<AccionInvesting> accionInvestingLista = new List<AccionInvesting>();

        public DataSetInvesting()
        {
        }

        public List<AccionInvesting> AccionInvestingLista { get => accionInvestingLista; set => accionInvestingLista = value; }


        public void ObtenerDatosInvesting(string CSVpath)
        {
            CargarDatosCSVInvesting(CSVpath);
            //DescargarDatosInvesting();
        }

        public void ObtenerDatosInvestingParaRobot(string CSVpath)
        {
            CargarDatosCSVInvesting(CSVpath);
            DescargarDatosInvestingConAnalisis();
        }


        public void CargarDatosCSVInvesting(string CSVpath)
        {
            DataSetInvesting conjuntoAcciones = new DataSetInvesting();
            Dictionary<string, string> diccionario = LectorCSVServicio.ReadInvestingCSVFile(CSVpath);

            foreach (KeyValuePair<string, string> elemento in diccionario)
            {
                AccionInvesting accion = new AccionInvesting(elemento.Key, elemento.Value);
                AccionInvestingLista.Add(accion);
            }

            Console.WriteLine("Se han cargado un total de " + diccionario.Count + " valores de URLS de Investing");
        }


        private void DescargarDatosInvesting()
        {

            string contenido = String.Empty;

            Console.WriteLine("");
            foreach (AccionInvesting accion in AccionInvestingLista)
            {
                Console.Write(ImpresionBaseServicio.FormateAStringAAnchoFijo("Capturando acción: " + accion.Nombre, 30));
                contenido = WebCatcher.CapturaValores(accion.UrlAccionInvesting);

                AccionInvesting accionCapturada = new AccionInvesting();
                accionCapturada = ExtractorInvesting.ParseaContenidoInvesting(contenido, accion);

                Console.WriteLine("    OK");

                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine(accionCapturada.Nombre);
                Console.ResetColor();
                Console.WriteLine(accionCapturada.ValorActual);
                Console.WriteLine(accionCapturada.DiferenciaMercado);
                Console.WriteLine(accionCapturada.PorcentajeEvolucion);

            }

            GC.Collect();


        }


        public AccionInvesting DescargaValoresAccion(AccionInvesting accion)
        {
            AccionInvesting retorno = new AccionInvesting(accion.Nombre, accion.UrlAccionInvesting);
            InstrumentPrice ip=ScrappingServicio.ScrapearURLInvesting(accion.UrlAccionInvesting);
            retorno.SetValoresInvesting(accion.Nombre, ip.ultimoValor, ip.variacionPrecio, ip.variacionPorcentaje);
            return retorno;
        }

        public AccionInvesting DescargaValoresAccionClasico(AccionInvesting accion)
        {

            string contenido = String.Empty;
            contenido = WebCatcher.CapturaValores(accion.UrlAccionInvesting);
            AccionInvesting retorno = new AccionInvesting();
            if (contenido != null)
            {
                retorno = ExtractorInvesting.ParseaContenidoInvesting(contenido, accion);
            }

            return retorno;
        }

        private List<AccionInvesting> DescargarDatosInvestingConAnalisis()
        {

            List<AccionInvesting> listaAcciones = new List<AccionInvesting>();
            string contenido = String.Empty;

            foreach (AccionInvesting accion in AccionInvestingLista)
            {
                AccionInvesting accionCapturada = DescargaDatosAccionInvesting(accion);
                listaAcciones.Add(accionCapturada);

            }

            GC.Collect();
            return listaAcciones;

        }

        private static AccionInvesting DescargaDatosAccionInvesting(AccionInvesting accion)
        {
            Console.Write(ImpresionBaseServicio.FormateAStringAAnchoFijo("Capturando acción: " + accion.Nombre, 30));
            String contenido = WebCatcher.CapturaValores(accion.UrlAccionInvesting);
            AccionInvesting accionCapturada = new AccionInvesting();
            accionCapturada = ExtractorInvesting.ParseaContenidoInvesting(contenido, accion);
            Console.WriteLine("  - Completado");
            return accionCapturada;

        }
    }
}
