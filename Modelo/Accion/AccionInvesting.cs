using System;
using System.Collections.Generic;
using System.Text;

namespace TheTrader.Modelo
{
    public class AccionInvesting
    {
        private string nombre;
        private string idAccionInvesting;
        private string urlAccionInvesting;

        private float valorActual;
        private float diferenciaMercado;
        private float porcentajeEvolucion;

        public AccionInvesting() { }

        public AccionInvesting(string nombre, string urlAccionInvesting)
        {
            this.nombre = nombre;
            this.urlAccionInvesting = urlAccionInvesting;
        }


        public void SetValoresInvesting(string nombre, float valorActual, float diferenciaMercado, float porcentajeEvolucion)
        {
            this.nombre = nombre;
            this.valorActual = valorActual;
            this.diferenciaMercado = diferenciaMercado;
            this.porcentajeEvolucion = porcentajeEvolucion;
        }

        public void SetValoresInvesting(string nombre, double valorActual, double diferenciaMercado, double porcentajeEvolucion)
        {
            this.nombre = nombre;
            this.valorActual = float.Parse(valorActual.ToString());
            this.diferenciaMercado = float.Parse(diferenciaMercado.ToString());
            this.porcentajeEvolucion = float.Parse(porcentajeEvolucion.ToString());
        }



        public string Nombre { get => nombre; set => nombre = value; }


        public string IdAccionInvesting { get => idAccionInvesting; set => idAccionInvesting = value; }
        public string UrlAccionInvesting { get => urlAccionInvesting; set => urlAccionInvesting = value; }
        public float ValorActual { get => valorActual; set => valorActual = value; }
        public float DiferenciaMercado { get => diferenciaMercado; set => diferenciaMercado = value; }
        public float PorcentajeEvolucion { get => porcentajeEvolucion; set => porcentajeEvolucion = value; }
    }
}
