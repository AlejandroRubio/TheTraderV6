using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TheTrader.Servicios
{
    public class GestorFicherosPlanosServicio
    {

        public static String GetLatestWriteTimeFromFileInDirectory(String path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            if (directoryInfo == null || !directoryInfo.Exists)
                return null;

            FileInfo[] files = directoryInfo.GetFiles();
            DateTime lastWrite = DateTime.MinValue;
            String filename = "";

            foreach (FileInfo file in files)
            {
                if (file.LastWriteTime > lastWrite)
                {
                    lastWrite = file.LastWriteTime;
                    filename = file.Name;
                }
            }

            return filename;
        }


        // a partir de un path carga en un único string el fichero 
        //utilizado en el programa para cargar los ficheros sql
        public static string LeeFichero(String filepath)
        {

            string text = System.IO.File.ReadAllText(filepath);
            string[] lines = System.IO.File.ReadAllLines(filepath);
            string returnString = "";

            foreach (string line in lines)
            {
                //Importante: llevan un salto de linea
                returnString = returnString + " \n " + line;
            }

            return returnString;
        }


        // a partir de un path carga en un único array de strings el fichero 
        public static string[] LeeLineasFichero(String filepath)
        {

            string text = System.IO.File.ReadAllText(filepath);
            string[] lines = System.IO.File.ReadAllLines(filepath);

            return lines;
        }

        public static List<String> LeeLineasFicheroALista(String filepath)
        {

            string text = System.IO.File.ReadAllText(filepath);
            string[] lines = System.IO.File.ReadAllLines(filepath);

            return lines.OfType<String>().ToList(); 
        }

        public static void EscribeContenidoAFichero(List<String> contenido, String path) {

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
            {
                foreach (string line in contenido)
                {
                        file.WriteLine(line);
                }
            }


        }
    }
}
