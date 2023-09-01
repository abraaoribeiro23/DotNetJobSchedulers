using Application.Common.Interfaces;
using Application.Dkron;
using Infrastructure.Dkron;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Infrastructure.Dkron.Tests;

public class ServiceProviderFixture
{
    public ServiceProviderFixture()
    {
        var services = new ServiceCollection();

        //IConfiguration configuration = new ConfigurationBuilder()
        //    .AddJsonFile("appsettings.test.json")
        //    .Build();

        services.AddScoped<IDkronService, DkronService>();
        services.AddHttpClient<IDkronService, DkronService>(c => c.BaseAddress = new Uri("http://localhost:8080/"));

        services.AddScoped<ISqlDataManager, DkronDataManager>();

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