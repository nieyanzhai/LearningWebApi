using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace LearningWebApi.Infrastructure.Data.DbContext;

public class PostgreSqlDbContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public PostgreSqlDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("PostgreSql");
    }

    public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
}