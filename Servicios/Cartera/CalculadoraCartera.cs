using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TheTrader.Configuracion;
using TheTrader.Controles.Cartera;
using TheTrader.Modelo;
using TheTrader.Servicios.Cartera;
using TheTrader.Utilidades;

namespace TheTrader.Controles
{
    class CalculadoraCartera
    {
        DataSetCartera valoresEnCartera;
        DataSetInvesting valoresInvesting;

        public CalculadoraCartera()
        {
            valoresEnCartera = new DataSetCartera();
            valoresInvesting = new DataSetInvesting();
        }

        public DataSetCartera ValoresEnCartera { get => valoresEnCartera; set => valoresEnCartera = value; }
        public DataSetInvesting ValoresInvesting { get => valoresInvesting; set => valoresInvesting = value; }


        public  void CalculaValorCartera()
        {
            //Cargar mis acciones
            valoresEnCartera.ObtenerValoresAccionesPropias(false);
            string csvEspanaCompleto = ConfiguracionAplicacion.ObtenerRutaCSVInvesting();
            valoresInvesting.CargarDatosCSVInvesting(csvEspanaCompleto);
            RecalculaMiCartera();

            //Carga de la tabla detalle_inversiones
            CalculadoraFondos fondosCalc = new CalculadoraFondos();
            fondosCalc.CalcularRentabilidadFondos();


            Console.ReadKey();
        }

        public List<AccionEnCartera> IntegraComprasYVentas(List<AccionEnCartera> dataSetCompras, List<AccionEnCartera> dataSetVentas)
        {

            List<AccionEnCartera> dataSetUnion = new List<AccionEnCartera>();
            dataSetUnion = UnificacionAccionesEnCartera(dataSetCompras);

            foreach (AccionEnCartera item in dataSetCompras)
            {
                AccionEnCartera encontrada = dataSetVentas.FirstOrDefault(o => o.Nombre == item.Nombre &&  o.IdOperacion == item.IdOperacion);
                if (encontrada != null)
                {
                    dataSetUnion.Remove(item);
                    item.AccionEnVenta(encontrada.Nombre, encontrada.ValorVenta, encontrada.FechaVenta, encontrada.NumeroAccionesVendidas, encontrada.ComisionVenta);
                    item.ProcesaTotal();
                    dataSetUnion.Add(item);
                }

                if (item.NumeroAccionesRestantes==0) {
                    dataSetUnion.Remove(item);
                }

            }

            return dataSetUnion;
        }

        public List<AccionEnCartera> ObtieneOperacionesCerradas(List<AccionEnCartera> dataSetCompras, List<AccionEnCartera> dataSetVentas)
        {
            List<AccionEnCartera> dataSetUnion = new List<AccionEnCartera>();
            dataSetUnion = UnificacionAccionesEnCartera(dataSetCompras);

            List<AccionEnCartera> dataSetCerradas = new List<AccionEnCartera>();


            foreach (AccionEnCartera item in dataSetCompras)
            {
                AccionEnCartera encontrada = dataSetVentas.FirstOrDefault(o => o.Nombre == item.Nombre && o.IdOperacion == item.IdOperacion);
                if (encontrada != null)
                {
                    //dataSetUnion.Remove(item);
                    item.AccionEnVenta(encontrada.Nombre, encontrada.ValorVenta, encontrada.FechaVenta, encontrada.NumeroAccionesVendidas, encontrada.ComisionVenta);
                    item.ProcesaTotal();            
                    //dataSetUnion.Add(item);
                }

                if (item.NumeroAccionesRestantes == 0)
                {
                    item.CalculaBeneficio();
                    dataSetUnion.Remove(item);
                    dataSetCerradas.Add(item);
                }

            }

            return dataSetCerradas;
        }

        private List<AccionEnCartera> UnificacionAccionesEnCartera(List<AccionEnCartera> dataSetCompras)
        {

            List<AccionEnCartera> dataSetUnion = new List<AccionEnCartera>();
            dataSetUnion.AddRange(dataSetCompras);
            return dataSetUnion;

        }


        public void RecalculaMiCartera()
        {
            List<AccionEnCartera> valoresActuales = new List<AccionEnCartera>();

            DataSetInvesting valoresInvestingFiltrado = new DataSetInvesting();
            try {

                int numeroAccionesEnCartera = valoresEnCartera.DataSetCarteraLista.Count;
                int contadorProcesando = 1;

                Dictionary<string, AccionInvesting> datosDeInvesting = new Dictionary<string, AccionInvesting>();

                foreach (AccionEnCartera enCartera in valoresEnCartera.DataSetCarteraLista.OrderBy(x=>x.Nombre))
                {
                    int porcentaje = Convert.ToInt32(contadorProcesando * 100 / numeroAccionesEnCartera); 

                    Console.Write("PROCESANDO "+ porcentaje+"% -> " + enCartera.Nombre);
                    AccionInvesting encontrada = valoresInvesting.AccionInvestingLista.Where(x => x.Nombre.ToUpper().Equals(enCartera.Nombre.ToUpper())).ToList().First();

                    //aqui evaluar si ya la he calculado antes o la tengo que descargar
                    AccionInvesting descargada;
                    if (datosDeInvesting.ContainsKey(encontrada.Nombre))
                    {
                        descargada = datosDeInvesting[encontrada.Nombre];
                    }
                    else {
                        descargada = valoresInvestingFiltrado.DescargaValoresAccion(encontrada);
                        datosDeInvesting.Add(encontrada.Nombre,descargada);
                    }
                    
                    
                    
                    enCartera.ValorActual = descargada.ValorActual;

                    //20200831 ACTUALIZACIÓN PARA ACCIONES CON CONTRA/SPLIT

                    //if (enCartera.Nombre.Equals("APPLE"))
                    //{
                    //    enCartera.ValorActual = enCartera.ValorActual * 4;
                    //}

                    enCartera.UltimaVariacion = descargada.PorcentajeEvolucion;
                    enCartera.ProcesaTotal();

                    valoresActuales.Add(enCartera);
                    //ImprimeValorActual(enCartera);
                    
                    ClearCurrentConsoleLine();
                    contadorProcesando++;

                    Thread.Sleep(1000);
                }


                ImprimeAccionesEnCartera(valoresActuales.OrderBy(x=>x.BeneficioTotalLimpio).ToList());
                InsercionCarteraSQL.TruncadoTablaAccionesEnCartera();
                InsercionCarteraSQL.InsertaBDAccionesEnCartera(valoresEnCartera.DataSetCarteraLista);

            }
            catch (System.InvalidOperationException e) {
              throw new Exception("Acción no encontrada en el listado de URLS");
            }
           
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        private void ImprimeAccionesEnCartera(List<AccionEnCartera> valoresActuales)
        {

            Console.WriteLine("");
            string cabecera = ImpresionBaseServicio.FormateAStringAAnchoFijoReverse("ACCIÓN              |   TOTAL COMPRA | TOTAL ACTUAL     ", 40);
            Console.WriteLine(cabecera);
            double total = 0;

            foreach (AccionEnCartera accion in valoresActuales)
            {
                Console.ResetColor();

                if (accion.ValorTotalActual > accion.ValorTotalCompra)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                


                //solo imprimo de aquellas que tengo acciones
                if (accion.NumeroAccionesRestantes > 0)
                {
       
                    Console.Write(ImpresionBaseServicio.FormateAStringAAnchoFijo(accion.Nombre, 20));
                    Console.Write("| " + ImpresionBaseServicio.FormateAStringAAnchoFijo(ImpresionBaseServicio.FormateADoubleAnchoFijo(accion.ValorTotalCompra.ToString()), 15));
                    Console.Write("| " + ImpresionBaseServicio.FormateAStringAAnchoFijo(ImpresionBaseServicio.FormateADoubleAnchoFijo(accion.ValorTotalActual.ToString()), 10));
                    Console.Write("\n");
                    total += accion.ValorTotalActual;
                }

            }

            Console.ForegroundColor = ConsoleColor.Red;
            total = Math.Round(total, 2);
            Console.WriteLine(ImpresionBaseServicio.FormateAStringAAnchoFijo("                                TOTAL| " + ImpresionBaseServicio.FormateADoubleAnchoFijo(total.ToString()), 30));



            Console.ResetColor();
        }


        private void ImprimeValorActual(AccionEnCartera accion) { 
            Console.Write(ImpresionBaseServicio.FormateAStringAAnchoFijo(accion.Nombre, 20));

       
            string valorTotalCompra = Double.Parse(accion.ValorTotalCompra.ToString()).ToString();
            if (!valorTotalCompra.Contains(",")) {
                valorTotalCompra = valorTotalCompra + ".00";
            }
            string valorTotalActual = Double.Parse(accion.ValorTotalActual.ToString()).ToString();
            if (!valorTotalActual.Contains(","))
            {
                valorTotalActual = valorTotalActual + ".00";
            }

            Console.Write("| " + ImpresionBaseServicio.FormateAStringAAnchoFijo(ImpresionBaseServicio.FormateADoubleAnchoFijo(valorTotalCompra), 10));
            Console.Write("| " + ImpresionBaseServicio.FormateAStringAAnchoFijo(ImpresionBaseServicio.FormateADoubleAnchoFijo(valorTotalActual), 10));
            Console.Write("\n");
        }
    }
}

        