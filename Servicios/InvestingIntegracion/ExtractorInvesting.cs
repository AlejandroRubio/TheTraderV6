using System;
using System.Collections.Generic;
using System.Text;
using TheTrader.Modelo;
using TheTrader.Utilidades;


namespace TheTrader.Controles.InvestingIntegracion
{
    class ExtractorInvesting
    {

        public static AccionInvesting ParseaContenidoInvesting(String contenido, AccionInvesting accion)
        {
            string identificador = ExtraerIdentificadorAccionInvesting(contenido);
            accion.IdAccionInvesting = identificador;


            int x1 = contenido.IndexOf(ConstantesParseoInvesting.DameCabecera(accion, 1));
            int tamanoBloque1 = ConstantesParseoInvesting.DameCabecera(accion, 1).Length;
            x1 += tamanoBloque1;
            string valorActual = contenido.Substring(x1, 5);


            int x2 = contenido.IndexOf(ConstantesParseoInvesting.DameCabecera(accion, 2));
            int tamanoBloque2 = ConstantesParseoInvesting.DameCabecera(accion, 2).Length;
            if (x2 == -1)
            {
                x2 = contenido.IndexOf(ConstantesParseoInvesting.DameCabecera(accion, 21));
                tamanoBloque2 = ConstantesParseoInvesting.DameCabecera(accion, 21).Length;
                if (x2 == -1)
                {
                    x2 = contenido.IndexOf(ConstantesParseoInvesting.DameCabecera(accion, 22));
                    tamanoBloque2 = ConstantesParseoInvesting.DameCabecera(accion, 22).Length;
                    if (x2 == -1)
                    {
                        x2 = contenido.IndexOf(ConstantesParseoInvesting.DameCabecera(accion, 23));
                        tamanoBloque2 = ConstantesParseoInvesting.DameCabecera(accion, 23).Length;
                    }
                }
            }

            x2 += tamanoBloque2;
            string valorActual2 = contenido.Substring(x2, 6);



            int x3 = contenido.IndexOf(ConstantesParseoInvesting.DameCabecera(accion, 3));
            int tamanoBloque3 = ConstantesParseoInvesting.DameCabecera(accion, 1).Length;
            if (x3 == -1)
            {
                x3 = contenido.IndexOf(ConstantesParseoInvesting.DameCabecera(accion, 31));
                tamanoBloque3 = ConstantesParseoInvesting.DameCabecera(accion, 31).Length;
                if (x3 == -1)
                {
                    x3 = contenido.IndexOf(ConstantesParseoInvesting.DameCabecera(accion, 32));
                    tamanoBloque3 = ConstantesParseoInvesting.DameCabecera(accion, 32).Length;
                    if (x3 == -1)
                    {
                        x3 = contenido.IndexOf(ConstantesParseoInvesting.DameCabecera(accion, 33));
                        tamanoBloque3 = ConstantesParseoInvesting.DameCabecera(accion, 33).Length;
                    }
                }
            }


            x3 += tamanoBloque3;
            string valorActual3 = contenido.Substring(x3, 6);

            accion.SetValoresInvesting(accion.Nombre, Formatadores.DameFloatFromString(valorActual), Formatadores.DameFloatFromString(valorActual2), Formatadores.DameFloatFromString(valorActual3));




            return accion;
        }

        private static string ExtraerIdentificadorAccionInvesting(string contenido)
        {
            int indiceIdInicio = contenido.IndexOf(ConstantesParseoInvesting.cabecera_ID_inicio) + ConstantesParseoInvesting.cabecera_ID_inicio.Length;
            int indiceIdFin = contenido.IndexOf(ConstantesParseoInvesting.cabecera_ID_fin);
            string identificadorObtenidoInvesting = contenido.Substring(indiceIdInicio, indiceIdFin - indiceIdInicio);
            identificadorObtenidoInvesting = identificadorObtenidoInvesting.Replace(" ", "");
            identificadorObtenidoInvesting = identificadorObtenidoInvesting.Replace("/", "");
            identificadorObtenidoInvesting = identificadorObtenidoInvesting.Replace("\"", "");
            identificadorObtenidoInvesting = identificadorObtenidoInvesting.Replace(">", "");
            identificadorObtenidoInvesting = identificadorObtenidoInvesting.Replace("\n", "");
            identificadorObtenidoInvesting = identificadorObtenidoInvesting.Replace("\t", "");
            return identificadorObtenidoInvesting;
        }

    }
}
