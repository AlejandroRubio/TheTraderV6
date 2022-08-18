using System;
using System.Collections.Generic;
using System.Linq;
using TheTrader.Configuracion;
using TheTrader.Controles;
using TheTrader.Controles.Cartera;
using TheTrader.Controles.Cartera.RecuperacionAccionesEnCartera;
using TheTrader.Utilidades;

namespace TheTrader.Modelo
{
    public class DataSetCartera
    {

        public List<AccionEnCartera> AccionesEnCartera = new List<AccionEnCartera>();

        public DataSetCartera()
        {
        }

        public List<AccionEnCartera> DataSetCarteraLista { get => AccionesEnCartera; set => AccionesEnCartera = value; }

        public void ObtenerValoresAccionesPropias(bool insercionDeCSVaBD)
        {
            IRecuperacionAccionesEnCartera recuperadorBD;

            if (insercionDeCSVaBD)
            {
                recuperadorBD = new RecuperacionAccionesCSV();
            }
            else {
                recuperadorBD = new RecuperacionAccionesBD();
            }
            
            //RecuperacionAccionesCSV recuperadorBD = new RecuperacionAccionesCSV();

            //carga valores de compras de CSV o BD
            List<AccionEnCartera> accionesCompras = new List<AccionEnCartera>();
            accionesCompras = recuperadorBD.CargarAccionesCompradas();
            Console.WriteLine("Cargadas un total de " + accionesCompras.Count + " compras.");


            //carga valores de ventas de CSV o S
            List<AccionEnCartera> accionesVentas = new List<AccionEnCartera>();
            accionesVentas = recuperadorBD.CargarAccionesVendidas();
            Console.WriteLine("Cargadas un total de " + accionesVentas.Count + " ventas.");
            Console.WriteLine("");

            if (insercionDeCSVaBD) {
                TraspasoCarteraCSVaBD.InsertaEnBDAccionesEnCartera(accionesCompras, true);
                TraspasoCarteraCSVaBD.InsertaEnBDAccionesEnCartera(accionesVentas, false);
            }
            else
            {
                //integra ventas y compras
                CalculadoraCartera calculadora = new CalculadoraCartera();

                //Operaciones cerradas
                List<AccionEnCartera> DataSetCarteraCerradas = calculadora.ObtieneOperacionesCerradas(accionesCompras, accionesVentas);

                InsercionCarteraSQL.TruncadoTablaAccionesCerradas();
                InsercionCarteraSQL.InsertaBDAccioneCerradas(DataSetCarteraCerradas);

                //acciones que tendría aún en cartera
                DataSetCarteraLista = calculadora.IntegraComprasYVentas(accionesCompras, accionesVentas);
                AccionesEnCartera=AccionesEnCartera.OrderBy(x=>x.ValorTotalCompra).ToList();
                //impresion de valores iniciales
                ImprimeAccionesEnCartera();
            }

        }


        private void ImprimeAccionesEnCartera()
        {

            Console.ForegroundColor = ConsoleColor.Blue;
            string cabecera = ImpresionBaseServicio.FormateAStringAAnchoFijoReverse("ACCIÓN              | NÚMERO ACCIONES | TOTAL     ", 40);
            Console.WriteLine(cabecera);
            double total = 0;

            foreach (AccionEnCartera accion in AccionesEnCartera)
            {
                //solo imprimo de aquellas que tengo acciones
                if (accion.NumeroAccionesRestantes > 0)
                {
                  
                    Console.Write(ImpresionBaseServicio.FormateAStringAAnchoFijo(accion.Nombre, 20));
                    Console.Write("| " + ImpresionBaseServicio.FormateAStringAAnchoFijo(accion.NumeroAccionesRestantes.ToString(), 15));
                    Console.Write("| " + ImpresionBaseServicio.FormateAStringAAnchoFijo(ImpresionBaseServicio.FormateADoubleAnchoFijo(accion.ValorTotalActual.ToString()), 10));
                    Console.Write("\n");
                    total = total+ Math.Round(accion.ValorTotalActual,2);
                }

            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(ImpresionBaseServicio.FormateAStringAAnchoFijo("                               TOTAL |  " + ImpresionBaseServicio.FormateADoubleAnchoFijoTotal(Math.Round(total,2).ToString()), 30));



            Console.ResetColor();
        }

    }
}
