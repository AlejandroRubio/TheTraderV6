using System;
using System.Collections.Generic;
using System.Text;

namespace TheTrader.Modelo.RoboAdvisor
{
    class AlertaRoboAdvisor
    {
        string texto;
        int nivelCriticidad;
        AlertasTipos tipoAlerta;

        public AlertaRoboAdvisor() {}



        public AlertaRoboAdvisor(string texto, int nivelCriticidad, AlertasTipos tipoAlerta)
        {
            this.texto = texto;
            this.nivelCriticidad = nivelCriticidad;
            this.tipoAlerta = tipoAlerta;
        }

        public string Texto { get => texto; set => texto = value; }
        public int NivelCriticidad { get => nivelCriticidad; set => nivelCriticidad = value; }
        public AlertasTipos TipoAlerta { get => tipoAlerta; set => tipoAlerta = value; }


        public void ImprimeAlertaRoboAdvisor()
        {
            string mensaje= tipoAlerta.ToString();

            switch (nivelCriticidad)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;

            }
            
            Console.WriteLine(mensaje + " - "+ texto);
            Console.ResetColor();
            throw new Exception(mensaje + " - " + texto);

        }

    }
}
