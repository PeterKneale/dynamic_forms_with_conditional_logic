using Core.Db;
using Core.Db.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Core.UnitTests;

public class IntegrationTestFixture : MartinCostello.Logging.XUnit.ITestOutputHelperAccessor
{
    public IntegrationTestFixture()
    {
        Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        Services = new ServiceCollection()
            .AddCore(Configuration)
            .AddLogging(c=>c.AddXUnit(this))
            .BuildServiceProvider();
        
        DatabaseMigrator.MigrateDown(Configuration.GetDbConnectionString());
        DatabaseMigrator.MigrateUp(Configuration.GetDbConnectionString());
    }

    public ServiceProvider Services { get; set; }

    public IConfigurationRoot Configuration { get; set; }

    public ITestOutputHelper? OutputHelper { get; set; }
}