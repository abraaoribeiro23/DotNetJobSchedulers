using Application;
using Infrastructure.Dkron;
using Serilog;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Tests.SqlDataManager;

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

        services.AddScoped<ISqlDataManager, SqlDataManager>();

        var logConfiguration = new LoggerConfiguration()//Para exibir o console no terminal 
            .WriteTo.Debug()
            .WriteTo.Console()
            .WriteTo.File("TestLogs/log.txt"); //para registrar o console no log.txt

        Log.Logger = logConfiguration.CreateLogger();

        Sp = services.BuildServiceProvider();
    }

    public ServiceProvider Sp { get; }
}