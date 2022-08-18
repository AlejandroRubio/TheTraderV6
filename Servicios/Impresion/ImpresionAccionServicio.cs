using System;
using TheTrader.Modelo;
using TheTrader.Utilidades;

namespace TheTrader.Servicios.Impresion
{
    class ImpresionAccionServicio
    {


        public static void ImprimeDatosAccionInvestingCapturada(AccionInvesting accionCargada)
        {
            string salida = string.Empty;
            salida += "|";
            salida += ImpresionBaseServicio.FormateAStringAAnchoFijo(accionCargada.Nombre,20);
            salida += "|";
            salida += ImpresionBaseServicio.FormateADoubleAnchoFijo(accionCargada.ValorActual)+"€";
            salida += "|";
            salida += ImpresionBaseServicio.FormateADoubleAnchoFijo(accionCargada.PorcentajeEvolucion) + "%";
            salida += "|";
            //Console.WriteLine( + ": " + accionCargada.ValorActual + "€" + "  -> VAR:" + accionCargada.PorcentajeEvolucion);.


            if (accionCargada.PorcentajeEvolucion>0) {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else{
                Console.ForegroundColor = ConsoleColor.Red;
            }
            
            Console.WriteLine(salida);
            Console.ResetColor();


            
        }

    }
}
