using Microsoft.Data.Sqlite;
using Quartz;

namespace WebApi.Extensions;

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
        command.CommandText = File.ReadAllText(@"SQLiteMigration\tables_sqlite.sql");
        command.ExecuteScalarAsync();
    }
}