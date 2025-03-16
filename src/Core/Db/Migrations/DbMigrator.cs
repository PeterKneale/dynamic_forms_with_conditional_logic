using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Db.Migrations;

public class DatabaseMigrator()
{
    /// <summary>
    /// Applies all pending migrations.
    /// </summary>
    public static void MigrateUp(string connectionString)
    {
        using var serviceProvider = CreateServices(connectionString);
        using var scope = serviceProvider.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

        Console.WriteLine("Applying database migrations...");
        runner.MigrateUp();
        Console.WriteLine("Migrations completed successfully.");
    }

    /// <summary>
    /// Rolls back migrations down to a specific version.
    /// </summary>
    public static void MigrateDown(string connectionString)
    {
        using var serviceProvider = CreateServices(connectionString);
        using var scope = serviceProvider.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

        Console.WriteLine($"Rolling back database");
        runner.MigrateDown(0);
        Console.WriteLine($"Rolled back successfully.");
    }

    /// <summary>
    /// Creates and configures the FluentMigrator service provider.
    /// </summary>
    private static ServiceProvider CreateServices(string connectionString)
    {
        return new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider();
    }
}