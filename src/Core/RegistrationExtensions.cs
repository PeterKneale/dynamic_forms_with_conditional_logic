using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class RegistrationExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);
        services.AddSingleton<DbConnectionFactory>();
        services.AddScoped<SessionRepository>();
        services.AddScoped<FormRepository>();
        services.AddScoped<ExpressionEvaluator>();
        services.AddScoped<FormService>();
        return services;
    }
}