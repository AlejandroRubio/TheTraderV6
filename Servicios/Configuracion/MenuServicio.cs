﻿using System;
using TheTrader.Configuracion;
using TheTrader.Controles;
using TheTrader.Controles.Alphavantage;
using TheTrader.Controles.InvestingIntegracion;
using TheTrader.Controles.YahooFinances;
using TheTrader.Modelo;
using TheTrader.Modelo.RoboAdvisor;
using TheTrader.Servicios.Calculadora;

namespace TheTrader.Servicios.Configuracion
{
    class MenuServicio
    {

        public static int PintarMenu()
        {

            Console.Clear();
            Console.WriteLine("---------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("|      TheTrader App ® By Alex Rubio              |");
            Console.ResetColor();
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("|  1. Calcular cotización cartera acciones        |");
            Console.WriteLine("|  2. Lanzar RoboAdvisor IBEX 35                  |");
            Console.WriteLine("|  3. Lanzar RoboAdvisor bolsa española completa  |");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("| 10. (A) Generación CSV Bolsa española           |");
            Console.WriteLine("| 11. (A) Cargar CSVs                             |");
            Console.WriteLine("| 12. (A) Advantage Modulo                        |");
            Console.WriteLine("| 13. (A) Cargar CSVs de Investing                |");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("| 20. (T) Batería pruebas sobre RoboAdvisor       |");
            Console.WriteLine("| 21. Cargar cartera CSV a BD                     |");
            Console.WriteLine("| 22. Cargar listado de valores de Investing      |");
            Console.WriteLine("| 23. (T) Test HTTPClient                         |");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("|  0. Cerrar programa                             |");
            Console.WriteLine("---------------------------------------------------");
            var result = Console.ReadLine();

            int enteroMenu = 0;
            try
            {
                enteroMenu = Convert.ToInt32(result);
            }
            catch
            {
                enteroMenu = 0;
            }

            return enteroMenu;
        }

        public static void EjecucionMenu(DataSetCartera valoresEnCartera, DataSetInvesting valoresInvesting)
        {
            int opcionMenuSeleccionada = 0;
            do
            {
                opcionMenuSeleccionada = PintarMenu();
                switch (opcionMenuSeleccionada)
                {
                    case 1:
                        //Carga cartera (compras - ventas)
                        CalculadoraCartera cartera = new CalculadoraCartera();
                        cartera.CalculaValorCartera();
                        break;
                    case 2:
                        //RoboAdvisor IBEX 35
                        RoboAdvisior ibex = new RoboAdvisior(RoboAdvisorTipoMercado.Ibex35);
                        ibex.EjecutarProcesadoDeRoboAdvisor();
                        break;
                    case 3:
                        //RoboAdvisor Bolsa Española
                        RoboAdvisior españa = new RoboAdvisior(RoboAdvisorTipoMercado.BolsaEspañolaCompleta);
                        españa.EjecutarProcesadoDeRoboAdvisor();
                        break;
                    case 10:
                        //Generación CSV España
                        ProcesadoHTMLInvesting cargador = new ProcesadoHTMLInvesting();
                        cargador.GeneracionCSVAcciones();
                        break;
                    case 11:
                        //Generación CSV España
                        YahooFinancesLoader.CargarCSVYahoo();
                        break;
                    case 12:
                        //Generación CSV España
                        AlphavantageIntegration alpha = new AlphavantageIntegration();
                        alpha.StartCheck();
                        break;
                    case 13:
                        CSVInvestingProcesador csvInvestingProcesador = new CSVInvestingProcesador();
                        csvInvestingProcesador.EjecutaCargaFicheroInvesting();
                        break;
                    case 20:
                        //TestingRoboAdvisor test = new TestingRoboAdvisor();
                        //test.EjecutaBateriaTest();
                        break;
                    case 21:
                        //Cargar mis acciones
                        valoresEnCartera.ObtenerValoresAccionesPropias(true);
                        break;
                    case 22:
                        //Obtener datos de Investing
                        string CSVpath = ConfiguracionAplicacion.ObtenerRutaCSVInvesting();
                        valoresInvesting.ObtenerDatosInvesting(CSVpath);
                        break;
                    case 23:
                        //ProcesamientoInvestingTest cliente = new ProcesamientoInvestingTest();
                        //cliente.PruebaHTTPClient();
                        break;
                    case 99:
                        //Calculo histórico de la cartera
                        CalculadoraRentaVariableService calculadoraService = new CalculadoraRentaVariableService();
                        calculadoraService.EjecutarCalculoHistoricoCartera();
                        break;
                    case 0:
                        break;
                }


            } while (opcionMenuSeleccionada != 0);
        }

    }
}