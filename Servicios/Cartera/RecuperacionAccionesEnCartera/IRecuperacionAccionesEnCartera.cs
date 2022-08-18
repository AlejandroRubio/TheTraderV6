using System;
using System.Collections.Generic;
using System.Text;
using TheTrader.Modelo;

namespace TheTrader.Controles.Cartera.RecuperacionAccionesEnCartera
{
    
    interface IRecuperacionAccionesEnCartera
    {
        List<AccionEnCartera> CargarAccionesCompradas();
        List<AccionEnCartera> CargarAccionesVendidas();
    }
}
