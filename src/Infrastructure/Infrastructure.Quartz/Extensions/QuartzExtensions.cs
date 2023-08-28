using System.Reflection;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Infrastructure.Quartz.Extensions;

public static class QuartzExtensions
{
    public static void AddQuartzService(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            CreateInitialDataBase();
            q.UsePersistentStore(options =>
            {
                options
                    .UseMicrosoftSQLite(providerOptions =>
                    {
                        providerOptions.ConnectionString = "Data Source=test.db";
                        providerOptions.TablePrefix = "QRTZ_";
                    });
                options.UseNewtonsoftJsonSerializer();
            });
        });
        services.AddQuartzHostedService(opt =>
        {
            opt.WaitForJobsToComplete = true;
        });
    }

    private static void CreateInitialDataBase()
    {
        using var connection = new SqliteConnection("Data Source=test.db");
        connection.Open();

        var command = connection.CreateCommand();

        var assemblyPath = Assembly.GetExecutingAssembly().Location;
        var assemblyDirectory = Path.GetDirectoryName(assemblyPath);
        var textPath = Path.Combine(assemblyDirectory!, "Data", "tables_sqlite.sql");
        command.CommandText = File.ReadAllText(textPath);
        command.ExecuteScalarAsync();
    }
}