using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Repository.Dapper;

public class DapperContext
{
    protected readonly IConfiguration configuration;
    protected readonly string connectionString;

    public DapperContext(IConfiguration configuration)
    {
        this.configuration = configuration;
        connectionString = ConnectionString();
    }
    public IDbConnection CreateConnection()
        => new SqlConnection(connectionString);

    private string ConnectionString()
    {
        string? MachineName = Environment.MachineName;
        string? ConnectionString = configuration.GetConnectionString("sqlConnection");
        ConnectionString = ConnectionString!.Replace("??????", MachineName);
        return ConnectionString;
    }
}
