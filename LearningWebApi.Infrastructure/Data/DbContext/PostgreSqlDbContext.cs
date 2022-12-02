using System.Data;
using Npgsql;

namespace LearningWebApi.Api.Services.Data.DbContext;

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