using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace TheTrader.Modelo.Investing
{
    class ValorInvestingCSV
    {
        public int id { get; set; }
        public String accion { get; set; }
        public DateTime fecha { get; set; }
        public double precio_ultimo { get; set; }
        public double precio_apertura { get; set; }
        public double precio_maximo { get; set; }
        public double precio_minimo { get; set; }
        public double volumen { get; set; }
        public double variacion { get; set; }

        public ValorInvestingCSV(String accion)
        {
            this.accion = accion;
        }


        public void ConvertRAW(RAW_ValorInvestingCSV valorCrudo)
        {
            fecha = DateTime.ParseExact(valorCrudo.fecha, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            precio_apertura = ProcesaDecimal(valorCrudo.precio_apertura);
            precio_minimo = ProcesaDecimal(valorCrudo.precio_minimo);
            precio_maximo = ProcesaDecimal(valorCrudo.precio_maximo);
            precio_ultimo = ProcesaDecimal(valorCrudo.precio_apertura);
            volumen = ProcesaDecimal(valorCrudo.volumen);
            variacion = ProcesaDecimal(valorCrudo.variacion);
        }

        private static double ProcesaDecimal(string valorString)
        {

            

            double valor = 0;

            //ampliacion 04/11/21
            if (Double.TryParse(valorString, out valor)) {
                return valor;
            }

            if (valorString is null)
            {
                return 0;
            }

            try
            {

                if (valorString.Equals("-"))
                {
                    return 0;
                }

                string cadena = valorString.Replace(".", ",");

                int multiplicador = 1;

                if (cadena.Contains("K"))
                {
                    multiplicador = 100;
                }

                cadena = Regex.Replace(cadena, "[^0-9.]", "");

                try
                {
                    valor = Double.Parse(cadena);

                }
                catch
                {
                    valor = 0;
                }

                valor = valor * multiplicador;
            }
            catch
            {
            }
            return valor;
        }
    }
}
