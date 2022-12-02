using LearningWebApi.Infrastructure.Data.Contract;
using LearningWebApi.Infrastructure.Data.DbContext;
using LearningWebApi.Infrastructure.Data.Repository;
using Microsoft.Extensions.DependencyInjection;


namespace LearningWebApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Add DbContext
        // builder.Services.AddSingleton<SqliteDbContext>();
        // builder.Services.AddScoped<IDataRepository, SqliteDataRepository>();

        services.AddSingleton<PostgreSqlDbContext>();
        services.AddScoped<IDataRepository, PostgreSqlDataRepository>();

        return services;
    }
}