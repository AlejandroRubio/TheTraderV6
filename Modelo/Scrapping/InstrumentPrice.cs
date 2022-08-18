using System;
using System.Collections.Generic;
using System.Text;

namespace TheTrader.Modelo.Scrapping
{
    public class InstrumentPrice
    {
       public string instrumentPriceLast;
       public string instrumentPriceChange;
       public string instrumentPriceChangePercent;

       public double ultimoValor;
       public double variacionPrecio;
       public double variacionPorcentaje;


        public InstrumentPrice(List<string> valoresBrutos) {

            if (valoresBrutos.Count==3) {
                instrumentPriceLast = valoresBrutos[0];
                instrumentPriceChange = valoresBrutos[1];
                instrumentPriceChangePercent = valoresBrutos[2];

                ultimoValor = Double.Parse(PurificaString(instrumentPriceLast));
                variacionPrecio = Double.Parse(PurificaString(instrumentPriceChange));
                variacionPorcentaje = Double.Parse(PurificaString(instrumentPriceChangePercent));
            }




        }


        private string PurificaString(string entrada) {
            string salida = entrada;
            salida= salida.Replace("(", "");
            salida = salida.Replace(")", "");
            salida = salida.Replace("%", "");
            return salida;

        }
    }
}
