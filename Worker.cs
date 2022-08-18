using Microsoft.Extensions.Configuration;

internal class Worker
{
    private readonly IConfiguration configuration;

    public Worker(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public void DoWork()
    {
        var keyValuePairs = configuration.AsEnumerable().ToList();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("==============================================");
        Console.WriteLine("Configurations...");
        Console.WriteLine("==============================================");
        foreach (var pair in keyValuePairs)
        {
            Console.WriteLine($"{pair.Key} - {pair.Value}");

        }
    }
}