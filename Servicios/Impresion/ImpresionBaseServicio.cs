using System;
using System.Collections.Generic;
using System.Text;

namespace TheTrader.Utilidades
{
    class ImpresionBaseServicio
    {


        public static String FormateAStringAAnchoFijo(String s, int ancho)
        {
            int anchodelincio = s.Length;
            for (int x = 0; x < ancho - anchodelincio; x++)
            {
                s = " " + s;
            }
            return s;
        }

        public static String FormateAStringAAnchoFijoReverse(String s, int ancho)
        {
            int anchodelincio = 0;
            try
            {
                anchodelincio = s.Length;
                for (int x = 0; x < ancho - anchodelincio; x++)
                {
                    s = s + " ";
                }
            }
            catch {
            }

            
            return s;
        }

        public static String FormateADoubleAnchoFijo(String s)
        {

         

            int ancho = 5;
            string[] words;

            if (!s.Contains(".") && !s.Contains(",")) {
                s = s + ".0";
            }

            if (s.Contains("."))
                words = s.Split('.');
            else
                words = s.Split(',');


            int anchodelincio = words.Length;
            String salida = words[0];

            for (int x = 0; x < ancho - anchodelincio; x++)
            {
                salida = " " + salida;
            }

            salida = salida + "." + words[1];
            return salida;
        }

        public static String FormateADoubleAnchoFijoTotal(String s)
        {



            int ancho = 4;
            string[] words;

            if (!s.Contains(".") && !s.Contains(","))
            {
                s = s + ".0";
            }

            if (s.Contains("."))
                words = s.Split('.');
            else
                words = s.Split(',');


            int anchodelincio = words.Length;
            String salida = words[0];

            for (int x = 0; x < ancho - anchodelincio; x++)
            {
                salida = " " + salida;
            }

            salida = salida + "." + words[1];
            return salida;
        }

        public static String FormateADoubleAnchoFijo(float f)
        {

            decimal dec = new decimal(f);
            double d = (double)dec; 


            String s = String.Format("{0:0.00}", d);


            int ancho = 6;
            string[] words = s.Split(',');
            int anchodelincio = words[0].Length;
            String salida = words[0];

            for (int x = 0; x < ancho - anchodelincio; x++)
            {
                salida = " " + salida;
            }

            salida = salida + "." + words[1];
            return salida;
        }


    }
}
