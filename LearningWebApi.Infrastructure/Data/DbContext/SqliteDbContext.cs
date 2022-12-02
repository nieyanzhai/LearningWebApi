using System.Data;
using System.Data.SQLite;

namespace LearningWebApi.Api.Services.Data.DbContext;

public class SqliteDbContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public SqliteDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("Sqlite");
    }

    public IDbConnection CreateConnection() => new SQLiteConnection(_connectionString);
}