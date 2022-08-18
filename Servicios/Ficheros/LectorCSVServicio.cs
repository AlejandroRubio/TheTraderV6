using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheTrader.Modelo;

namespace TheTrader.Controles
{
    class LectorCSVServicio
    {

        public static String GenericFileCSVReadString(String filepath, bool removeHeader)
        {
            List<String> fileContent = System.IO.File.ReadAllLines(filepath).ToList();

            if (removeHeader)
                fileContent.RemoveAt(0);

            

            return String.Join("\n", fileContent); ;
        }

        public static List<String> GenericFileCSVReadList(String filepath, bool removeHeader)
        {
            List<String> fileContent = System.IO.File.ReadAllLines(filepath).ToList();
            
            if(removeHeader)
            fileContent.RemoveAt(0);

            return fileContent;
        }

        public static Dictionary<string, string> ReadInvestingCSVFile(String filepath)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            using (var reader = new StreamReader(filepath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    string nombreAccion = values[0];
                    nombreAccion = nombreAccion.Trim();
                    string urlInvesting = values[1];
                    urlInvesting = urlInvesting.Trim();

                    dictionary.Add(nombreAccion, urlInvesting);
                }
            }

            return dictionary;
        }



    }
}
