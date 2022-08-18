using System;
using System.Collections.Generic;
using System.Text;

namespace TheTrader.Modelo
{
    public class AccionEnCartera
    {

        private string nombre;
        private int    numeroAccionesRestantes;
        private string idOperacion;

        private double  valorCompra;
        private int    numeroAccionesCompradas;
        private double  valorTotalCompra;
        private string fechaCompra;
        private double  comisionCompra;

        private double  valorVenta;
        private int    numeroAccionesVendidas;
        private double  valorTotalVenta;
        private string fechaVenta;
        private double  comisionVenta;

        private double  valorActual;
        private double  valorTotalActual;


        private double beneficioTotalLimpio;
        private double beneficioTotalSucio;


        private double ultimaVariacion;

        public AccionEnCartera(){ }

        public void AccionEnCompra(string nombre, double valorCompra, string fechaCompra, int numeroAccionesCompradas, double comisionCompra)
        {
            this.Nombre = nombre;
            this.ValorCompra = valorCompra;
            this.FechaCompra = fechaCompra;
            this.numeroAccionesCompradas = numeroAccionesCompradas;
            this.ComisionCompra = comisionCompra;
        }

        public void AccionEnVenta(string nombre, double valorVenta, string fechaVenta, int numeroAccionesVendidas, double comisionVenta)
        {
            this.Nombre = nombre;
            this.valorVenta = valorVenta;
            this.fechaVenta = fechaVenta;
            this.numeroAccionesVendidas = numeroAccionesVendidas;
            this.comisionVenta = comisionVenta;
        }


        public string Nombre { get => nombre; set => nombre = value; }
        public int NumeroAccionesRestantes { get => numeroAccionesRestantes; set => numeroAccionesRestantes = value; }
        public double ValorCompra { get => valorCompra; set => valorCompra = value; }
        public int NumeroAccionesCompradas { get => numeroAccionesCompradas; set => numeroAccionesCompradas = value; }
        public double ValorTotalCompra { get => valorTotalCompra; set => valorTotalCompra = value; }
        public string FechaCompra { get => fechaCompra; set => fechaCompra = value; }
        public double ComisionCompra { get => comisionCompra; set => comisionCompra = value; }
        public double ValorVenta { get => valorVenta; set => valorVenta = value; }
        public int NumeroAccionesVendidas { get => numeroAccionesVendidas; set => numeroAccionesVendidas = value; }
        public double ValorTotalVenta { get => valorTotalVenta; set => valorTotalVenta = value; }
        public string FechaVenta { get => fechaVenta; set => fechaVenta = value; }
        public double ComisionVenta { get => comisionVenta; set => comisionVenta = value; }
        public double ValorActual { get => valorActual; set => valorActual = value; }
        public double ValorTotalActual { get => valorTotalActual; set => valorTotalActual = value; }
        public string IdOperacion { get => idOperacion; set => idOperacion = value; }
        public double BeneficioTotalLimpio { get => beneficioTotalLimpio; set => beneficioTotalLimpio = value; }
        public double BeneficioTotalSucio { get => beneficioTotalSucio; set => beneficioTotalSucio = value; }
        public double UltimaVariacion { get => ultimaVariacion; set => ultimaVariacion = value; }



        public void ProcesaTotal()
        {
            numeroAccionesRestantes = numeroAccionesCompradas - numeroAccionesVendidas;

            if (NumeroAccionesVendidas!=0){
                // ValorTotalVenta = Math.Round((numeroAccionesVendidas * ValorVenta) - ComisionVenta,4);
                ValorTotalVenta = Math.Round((numeroAccionesVendidas * ValorVenta) , 2);
            }

            if (NumeroAccionesCompradas != 0)
            {
                // ValorTotalCompra = Math.Round((ValorCompra * NumeroAccionesCompradas) + ComisionCompra,4);
                ValorTotalCompra = Math.Round((ValorCompra * NumeroAccionesCompradas) , 2);
            }
           

            if (valorActual == 0)
            {
                valorTotalActual = Math.Round(numeroAccionesRestantes * valorCompra,2);
            }
            else {
                valorTotalActual = Math.Round(numeroAccionesRestantes * valorActual,2);
            }


         
            
        }


        public void CalculaBeneficio() {

            if (numeroAccionesRestantes==0) {
                //se han vendido todas las opciones
                BeneficioTotalLimpio = ValorTotalVenta - ValorTotalCompra;
                BeneficioTotalSucio = Math.Round((NumeroAccionesVendidas * ValorVenta) - (NumeroAccionesCompradas * ValorCompra),4);
            }

        }
    }
}
