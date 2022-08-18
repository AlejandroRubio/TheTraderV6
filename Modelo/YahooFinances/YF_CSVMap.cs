using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using TheTrader.Configuracion;
using TheTrader.Controles;
using TheTrader.Modelo.YahooFinances;
using TheTrader.Utilidades;

namespace TheTrader.Modelo
{

    public class YF_CSVMap : ClassMap<YF_CSV>
    {
        public YF_CSVMap()
        {
            Map(m => m.Date).Name("Date");
            Map(m => m.Open).Name("Open");
            Map(m => m.High).Name("High");
            Map(m => m.Low).Name("Low");
            Map(m => m.Close).Name("Close");
            Map(m => m.Adj_Close).Name("Adj Close");
            Map(m => m.Volume).Name("Volume");
        }

    }
}


 
