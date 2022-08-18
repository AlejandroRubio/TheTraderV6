using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TheTrader.Modelo.YahooFinances
{
    public class YF_CSV
    {

        [Name("Date")]
        public DateTime Date { get; set; }
        [Name("Open")]
        public double Open { get; set; }
        [Name("High")]
        public double High { get; set; }
        [Name("Low")]
        public double Low { get; set; }
        [Name("Close")]
        public double Close { get; set; }
        [Name("Adj Close")]
        public double Adj_Close { get; set; }
        [Name("Volume")]
        public int Volume { get; set; }


    }
}
