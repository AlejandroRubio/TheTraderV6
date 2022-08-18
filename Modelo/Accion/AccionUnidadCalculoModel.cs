using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheTrader.Modelo
{
    public class AccionUnidadCalculoModel
    {

        public AccionUnidadCalculoModel() { }

        public string id;
        public string accion;
        public DateTime fecha_compra;
        public DateTime? fecha_venta;

        public int num_acciones_compra;
        public double valor_compra;
        public double comision_compra;

        public double? valor_venta;
        public double? comision_venta;
        public int? num_acciones_venta;


    }
}
