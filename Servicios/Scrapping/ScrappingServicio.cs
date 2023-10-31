using HtmlAgilityPack;
using System.Globalization;
using System.Xml.Linq;
using TheTrader.Controles.Web;
using TheTrader.Modelo.Scrapping;

namespace TheTrader.Servicios.Scrapping
{
    public class ScrappingServicio
    {

        //const string clase = "'instrument-price_instrument-price'";
        //const string clase2 = "'top bold inlineblock'";// ACTUALIZACION 20210712
        //const string clase_valor = "'text-5xl font-bold leading-9'";// ACTUALIZACION 20230221
        const string clase_valor = "'text-5xl/9 font-bold'"; //ACTUALIZACION 20231026
        //const string clase_variacion = "'text-negative-main rtl:force-ltr'"; //ACTUALIZACION 20231031
        const string clase_variacion_porcentaje = "@data-test='instrument-price-change-percent'";
        const string clase_variacion_precio = "@data-test='instrument-price-change'";

        public static InstrumentPrice ScrapearURLInvesting(string url)
        {
            //Captura contenido de la web 
            string urlResponse = WebCatcher.CapturaValores(url);
            InstrumentPrice datosPrecio = ProcesaHTMLInvesting(urlResponse);

            return datosPrecio;
        }

        public static InstrumentPrice ProcesaHTMLInvesting(string urlResponse)
        {
            int contadorReintentos = 0;
            //Transformación del string con todo el HTML capturado al objeto
            InstrumentPrice datosPrecio = new InstrumentPrice();
            while (datosPrecio.ultimoValor == 0 && (contadorReintentos >= 0 && contadorReintentos < 2))
            {
                float valorPrecio = 0;
                float valorVariacionPorcentaje = 0;
                float valorVariacionPrecio = 0;

                contadorReintentos++;
                try
                {
                    valorPrecio = ExtraerElementoDesdeHTML("div", urlResponse, clase_valor);
                    datosPrecio.ultimoValor = valorPrecio;
                }
                catch
                {
                    Console.WriteLine("\nExcepcion críticoleyendo valor URL ");
                }

                try
                {
                    datosPrecio.variacionPorcentaje = ExtraerElementoDesdeHTML("span", urlResponse, clase_variacion_porcentaje);
                    datosPrecio.variacionPrecio = ExtraerElementoDesdeHTML("span", urlResponse, clase_variacion_precio);
                }
                catch
                {
                    Console.WriteLine("\nError recuperando variación ");
                }


            }

            if (datosPrecio == null)
            {
                //datosPrecio = ExtraerValoresDesdeHTMLClase3(urlResponse);
                List<string> listaCeros = new List<string>() { "0", "0", "0", "0", "0", "0" };
                datosPrecio = new InstrumentPrice(listaCeros);
                Console.WriteLine("\nError leyendo URL ");
            }

            return datosPrecio;
        }

        //tipo_elemento div o span
        public static float ExtraerElementoDesdeHTML(string tipo_elemento, string urlResponse, string claseAExtraer)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(urlResponse);

            //CON ESTO FUNCA
            //string clase = "instrument-price_instrument-price__3uw25 instrument-price_instrument-price-lg__3ES-Q";
            //HtmlNodeCollection divContainer = htmlDoc.DocumentNode.SelectNodes("//div[@class='"+ clase + "']");

            //Selección del nodo
            HtmlNodeCollection divContainer = null;

            if (tipo_elemento.Equals("div"))
            {
                divContainer = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, " + claseAExtraer + ")]");
            }

            if (tipo_elemento.Equals("span"))
            {
                divContainer = htmlDoc.DocumentNode.SelectNodes("//span[" + claseAExtraer + "]");
            }

            string IPIPdiv = divContainer.First().InnerText;
            //tratamiento adicional
            IPIPdiv = IPIPdiv.Replace("(", "").Replace(")", "");
            IPIPdiv = IPIPdiv.Replace("%", "");


            //bug ocn paypal: multiplica por 100 todos los valores el propio HTML
            float valor = 0;
            if (IPIPdiv.Contains("."))
            {
                valor = float.Parse(IPIPdiv.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
            }
            else
            {
                valor = float.Parse(IPIPdiv.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
            }

            return valor;
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

            try
            {
                float valorPrecio = float.Parse(IPIPdiv.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                InstrumentPrice valorUnico = new InstrumentPrice();
                valorUnico.ultimoValor = valorPrecio;
                return valorUnico;

            }
            catch
            {
                Console.WriteLine("ERROR en el scrapping");
            }


            InstrumentPrice valores = new InstrumentPrice(bueno);
            return valores;

        }




    }
}
