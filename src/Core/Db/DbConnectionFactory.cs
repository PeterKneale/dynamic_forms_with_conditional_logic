namespace Core.Db;

public class DbConnectionFactory(IConfiguration configuration)
{
    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(configuration.GetDbConnectionString());
    }
}