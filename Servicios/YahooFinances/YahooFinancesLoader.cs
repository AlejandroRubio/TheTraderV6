
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using TheTrader.Modelo;
using TheTrader.Modelo.YahooFinances;

namespace TheTrader.Controles.YahooFinances
{
    class YahooFinancesLoader
    {


        public static void CargarCSVYahoo()
        {

            string path = "C:\\MyTFS\\TheTrader\\TheTrader\\Configuracion\\CSVs\\SAN.MC.csv";
            List<YF_CSV> listado = new List<YF_CSV>();

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                //csv.Configuration.TypeConverterOptionsCache.GetOptions<DateTime>().Formats = new[] { "yyyy-MM-dd" };
                //csv.Configuration.RegisterClassMap<YF_CSVMap>();
                var records = csv.GetRecords<YF_CSV>();
                listado = records.ToList();
            }


            DateTime oneTwentyDaysAgo = DateTime.Today.AddDays(-120);
            var filtrofehca = listado.Where(x => x.Date > oneTwentyDaysAgo);

            double value= filtrofehca.Average(x=>x.Close);





        }
        
    }
}
