using System;
using System.Collections.Generic;
using System.Text;
using TheTrader.Configuracion;
using TheTrader.Modelo.RoboAdvisor;

namespace TheTrader.Modelo.Configuracion
{
    class RoboAdvisorConfiguracion
    {

        private string csvPathURLsInvesting;

        private RoboAdvisorTipoMercado modoConjunto;

        public RoboAdvisorConfiguracion() { }


        public RoboAdvisorConfiguracion(RoboAdvisorTipoMercado modoConjunto)
        {
            this.modoConjunto = modoConjunto;
            if (modoConjunto==RoboAdvisorTipoMercado.Ibex35)
            {
                CsvPathURLsInvesting = ConfiguracionAplicacion.ObtenerRutaCSVInvestingIBEX();
            }
            else {
                CsvPathURLsInvesting = ConfiguracionAplicacion.ObtenerRutaCSVInvestingEspana();
            }

        }

        public string CsvPathURLsInvesting { get => csvPathURLsInvesting; set => csvPathURLsInvesting = value; }
    }
}
