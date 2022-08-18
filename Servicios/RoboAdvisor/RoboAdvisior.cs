using System;
using System.Collections.Generic;
using System.Text;
using TheTrader.Controles.RoboAdvisor;
using TheTrader.Modelo;
using TheTrader.Modelo.Configuracion;
using TheTrader.Modelo.RoboAdvisor;
using TheTrader.Servicios.Impresion;

namespace TheTrader.Controles
{
    class RoboAdvisior
    {

        DataSetInvesting valoresInvesting = new DataSetInvesting();
  

        RoboAdvisorTipoMercado tipoMercado;

        public RoboAdvisior(RoboAdvisorTipoMercado modoConjunto)
        {
            this.tipoMercado = modoConjunto;
        }

        public void EjecutarProcesadoDeRoboAdvisor() {
            
            Console.Clear();
            RoboAdvisorConfiguracion configuracionRobot= new RoboAdvisorConfiguracion(tipoMercado);
            DataSetInvesting valoresPrecarga = new DataSetInvesting();
            valoresPrecarga.CargarDatosCSVInvesting(configuracionRobot.CsvPathURLsInvesting);

            throw new Exception("Cargados un total de "+ valoresPrecarga.AccionInvestingLista.Count+" acciones de investing");
  
            foreach (AccionInvesting accion in valoresPrecarga.AccionInvestingLista)
            {
                AccionInvesting accionCargada = valoresInvesting.DescargaValoresAccion(accion);
                ImpresionAccionServicio.ImprimeDatosAccionInvestingCapturada(accionCargada);
                valoresInvesting.AccionInvestingLista.Add(accionCargada);

                EjecucionReglasNegocio(accionCargada);
                throw new Exception("Procesada acción: " + accionCargada.Nombre);
            }
        }

     

        private void EjecucionReglasNegocio(AccionInvesting accionCargada)
        {
            RoboAdvisorReglasNegocio negocio = new RoboAdvisorReglasNegocio(accionCargada);
            negocio.EjecutaReglas();
        }

      

        private RoboAdvisorConfiguracion ConfiguraRoboAdvisorI() {

            RoboAdvisorConfiguracion configuracionRobot = new RoboAdvisorConfiguracion();
            return configuracionRobot;

        }




    }
}
