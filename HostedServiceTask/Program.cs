namespace HostedServiceTask;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var services = builder.Services;

        services.AddHostedService<TimePrinterService>();

        var app = builder.Build();

        app.Run();
    }
}