

using Contracts.Dapper;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Repository.Dapper;

public class DapperDeleteRepository : DapperContext, IDapperDeleteRepository
{
    public DapperDeleteRepository(IConfiguration config) : base(config) { }

    public async Task<int> DeleteItem(string query, object? condition)
    {
        using (var connection = CreateConnection())
        {
            return await connection.ExecuteAsync(query, condition);
        }
    }
}
