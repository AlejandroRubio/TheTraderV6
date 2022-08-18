using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace TheTrader.Utilidades
{
    class Formatadores
    {


      

        public static float DameFloatFromString(string numero) {
            float resultado = 0;
            numero = numero.Replace("+","");
            numero = numero.Replace("%", "");
            numero = numero.Replace(",", ".");
            numero = numero.Replace(" ", "");
            numero = numero.Replace(">", "");
            numero = numero.Replace("<", "");

            numero = Regex.Replace(numero, "[A-Za-z ]", "");
            try
            {
                resultado = float.Parse(numero, CultureInfo.InvariantCulture.NumberFormat);
            }
            catch(Exception e)
            {
                throw new Exception("Error con los datos float parseados de investing: "+ numero);
                resultado = 0;
            }

            return resultado;

        }
    }
}
