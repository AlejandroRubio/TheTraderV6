using System;
using System.Collections.Generic;
using System.Text;

namespace TheTrader.Modelo.AlphaVantage
{

    public class AlphaVantageData
    {
        public DateTime Timestamp { get; set; }
        public decimal Open { get; set; }

        public decimal High { get; set; }
        public decimal Low { get; set; }

        public decimal Close { get; set; }
        public decimal Volume { get; set; }
    }
}