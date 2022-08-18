using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using TheTrader.Controles.Web;
using TheTrader.Modelo.Scrapping;

namespace TheTrader.Servicios.Scrapping
{
    public class ScrappingServicio
    {

        const string clase = "'instrument-price_instrument-price'";
        const string clase2 = "'top bold inlineblock'";// ACTUALIZACION 20210712

        public static InstrumentPrice ScrapearURLInvesting(string url)
        {
            //Captura contenido de la web 
            string urlResponse = WebCatcher.CapturaValores(url);
            //Transformación del string con todo el HTML capturado al objeto
            InstrumentPrice datosPrecio = ExtraerValoresDesdeHTML(urlResponse);
            return datosPrecio;
        }

        public static InstrumentPrice ExtraerValoresDesdeHTML(string urlResponse)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(urlResponse);

            //CON ESTO FUNCA
            //string clase = "instrument-price_instrument-price__3uw25 instrument-price_instrument-price-lg__3ES-Q";
            //HtmlNodeCollection divContainer = htmlDoc.DocumentNode.SelectNodes("//div[@class='"+ clase + "']");

            //Selección del nodo
            HtmlNodeCollection divContainer = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, " + clase + ")]");
            string IPIPdiv = divContainer.First().InnerText;


            //bug ocn paypal: multiplica por 100 todos los valores el propio HTML
            bool candidadoAQueHayaPilladoMiles = false;
            if (IPIPdiv.Contains(".")) {
                candidadoAQueHayaPilladoMiles = true;
            }

            //Transformación de la cadena al objeto
            InstrumentPrice datosPrecio = TransformacionContenidoDelDIV(IPIPdiv);
            if (candidadoAQueHayaPilladoMiles) {
                datosPrecio.ultimoValor = datosPrecio.ultimoValor / 100;
                datosPrecio.variacionPorcentaje = datosPrecio.variacionPorcentaje / 100;
                datosPrecio.variacionPrecio = datosPrecio.variacionPrecio / 100;
            }
            

            return datosPrecio;
        }

        private static InstrumentPrice TransformacionContenidoDelDIV(string IPIPdiv)
        {
            List<string> bueno = new List<string>();
            if (IPIPdiv.Contains("\n"))
            {
                //Lo mas seguro que sea mi prueba
                string contenidoProcesado = IPIPdiv.Replace(" ", "");
                List<string> lineas = contenidoProcesado.Split("\n").ToList();


                foreach (string linea in lineas)
                {
                    if (!linea.Equals("") && !linea.Contains("&nbsp;"))
                    {
                        bueno.Add(linea);
                    }
                }
            }
            else
            {
                //respuesta reales tipo:
                //2,843 - 0,034(-1, 18 %)
                //58,65 + 0,41(+0, 70 %)
                string contenidoProcesado = IPIPdiv.Replace("+", "#");

                //comento la linea de abajo ya que sino las varaciones no las pilla negativas
                contenidoProcesado = contenidoProcesado.Replace("-", "#-");
                
                bueno = contenidoProcesado.Split("#").ToList();

            }


            InstrumentPrice valores = new InstrumentPrice(bueno);
            return valores;

        }

 


    }
}
