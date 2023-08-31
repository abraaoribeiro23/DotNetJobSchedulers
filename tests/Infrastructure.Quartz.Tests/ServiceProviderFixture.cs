using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Infrastructure.Quartz.Tests;

public class ServiceProviderFixture
{
    public ServiceProviderFixture()
    {
        var services = new ServiceCollection();

        //IConfiguration configuration = new ConfigurationBuilder()
        //    .AddJsonFile("appsettings.test.json")
        //    .Build();

        services.AddQuartzService();

        services.AddLogging(builder => builder.AddSerilog(dispose: true));

        var logConfiguration = new LoggerConfiguration()//Para exibir o console no terminal 
            .MinimumLevel.Error()
            .WriteTo.Debug()
            .WriteTo.Console()
            .WriteTo.File("TestLogs/log.txt"); //para registrar o console no log.txt

        Log.Logger = logConfiguration.CreateLogger();

        Sp = services.BuildServiceProvider();
    }

    public ServiceProvider Sp { get; }
}