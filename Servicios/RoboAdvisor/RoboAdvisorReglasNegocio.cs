using System;
using System.Collections.Generic;
using System.Text;
using TheTrader.Modelo;
using TheTrader.Modelo.RoboAdvisor;
using TheTrader.Utilidades;

namespace TheTrader.Controles.RoboAdvisor
{
    public class RoboAdvisorReglasNegocio
    {

        AccionInvesting accion = new AccionInvesting();

        public RoboAdvisorReglasNegocio(AccionInvesting accion)
        {
            this.accion = accion;
        }


        public void EjecutaReglas() {
            EjecutaReglaVariacionPorcentaje();
        }


        private string ConstruyeMensaje(AlertasTipos tipoAlerta) {

            string metrica = String.Empty;

            switch (tipoAlerta) {
                case AlertasTipos.AlertaVariacionPorcentaje:
                    metrica = "PORCENTAJE";
                    break;
                case AlertasTipos.AlertaVariacionValor:
                    metrica = "VALOR";
                    break;

            }

            return  ImpresionBaseServicio.FormateAStringAAnchoFijoReverse(accion.Nombre, 10)  + " Hora: " + DateTime.UtcNow.Date + " Métrica "+ metrica+ ": " + accion.PorcentajeEvolucion;
        }


        /// <summary>
        /// Regla 1: Análisis variación del porcentaje 
        ///     Se valida en tres nivel la variación que ha tenido la acción en el día
        ///         -Nivel aviso criticidad baja:  subida entre un 2% y un 5%
        ///         -Nivel aviso criticidad media: subida entre un 5% y un 10%
        ///         -Nivel aviso criticidad alta:  subida superior a un 10%
        /// </summary>
        /// <param name="accionCargada"></param>
        private void EjecutaReglaVariacionPorcentaje()
        {
            AlertaRoboAdvisor alerta = new AlertaRoboAdvisor() ;
            bool alertaActivada = false;

            string texto = ConstruyeMensaje(AlertasTipos.AlertaVariacionPorcentaje);

            if (accion.PorcentajeEvolucion > 10.0)
            {
                alerta = new AlertaRoboAdvisor(texto, 1, AlertasTipos.AlertaVariacionPorcentaje);
                alertaActivada = true;
            }
            else if (accion.PorcentajeEvolucion > 5.0)
            {
                alerta = new AlertaRoboAdvisor(texto, 2, AlertasTipos.AlertaVariacionPorcentaje);
                alertaActivada = true;
            }
            else if (accion.PorcentajeEvolucion > 2.0)
            {
                alerta = new AlertaRoboAdvisor(texto, 3, AlertasTipos.AlertaVariacionPorcentaje);
                alertaActivada = true;
            }

            if (alertaActivada){
                alerta.ImprimeAlertaRoboAdvisor();
            }
          
        }



        /// <summary>
        /// Regla 1: Análisis variación del porcentaje 
        ///     Se valida en tres nivel la variación que ha tenido la acción en el día
        ///         -Nivel aviso criticidad baja:  subida entre un 2% y un 5%
        ///         -Nivel aviso criticidad media: subida entre un 5% y un 10%
        ///         -Nivel aviso criticidad alta:  subida superior a un 10%
        /// </summary>
        /// <param name="accionCargada"></param>
        private void EjecutaReglaVariacionVariacionValor()
        {
            AlertaRoboAdvisor alerta = new AlertaRoboAdvisor();
            bool alertaActivada = false;

            string texto = ConstruyeMensaje(AlertasTipos.AlertaVariacionPorcentaje);

            if (accion.DiferenciaMercado > 1)
            {
                alerta = new AlertaRoboAdvisor(texto, 1, AlertasTipos.AlertaVariacionValor);
                alertaActivada = true;
            }
            else if (accion.DiferenciaMercado > 0.5)
            {
                alerta = new AlertaRoboAdvisor(texto, 2, AlertasTipos.AlertaVariacionValor);
                alertaActivada = true;
            }
            else if (accion.DiferenciaMercado > 0.25)
            {
                alerta = new AlertaRoboAdvisor(texto, 3, AlertasTipos.AlertaVariacionValor);
                alertaActivada = true;
            }

            if (alertaActivada)
            {
                alerta.ImprimeAlertaRoboAdvisor();
            }

        }


    }
}
