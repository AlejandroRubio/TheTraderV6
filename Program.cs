
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TheTrader.Modelo;
using TheTrader.Servicios.Configuracion;


// Setup Host
var host = CreateDefaultBuilder().Build();

// Invoke Worker
using IServiceScope serviceScope = host.Services.CreateScope();
IServiceProvider provider = serviceScope.ServiceProvider;
var workerInstance = provider.GetRequiredService<Worker>();
workerInstance.DoWork();

//Inicialización carteras y ejecución menú
DataSetCartera valoresEnCartera = new DataSetCartera();
DataSetInvesting valoresInvesting = new DataSetInvesting();
MenuServicio.EjecucionMenu(valoresEnCartera, valoresInvesting);

Console.WriteLine("FIN DE LA EJECUCIÓN");
Console.ReadKey();


static IHostBuilder CreateDefaultBuilder()
{
    return Host.CreateDefaultBuilder()
        .ConfigureAppConfiguration(app =>
        {
            app.AddJsonFile("appsettings.json");
        })
        .ConfigureServices(services =>
        {
            services.AddSingleton<Worker>();
        });
}