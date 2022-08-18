using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace TheTrader.Configuracion.Constantes
{
    public class ConstantesServicio
    {

        IConfigurationRoot configuration;
        public ConstantesServicio()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configuration = builder.Build();
        }

        public  string ObtenerSetting(String nombreClave)
        {
            return configuration.GetSection(nombreClave).Value;
        }

    }
}
