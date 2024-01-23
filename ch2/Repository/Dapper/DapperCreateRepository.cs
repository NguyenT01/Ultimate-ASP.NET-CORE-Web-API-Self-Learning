

using Contracts.Dapper;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Repository.Dapper;

public class DapperCreateRepository : DapperContext, IDapperCreateRepository
{
    public DapperCreateRepository(IConfiguration config) : base(config) { }

    public async Task<int> CreateItem<T>(string query, object itemParams)
    {
        using (var connection = CreateConnection())
        {
            //string queryAndReturnId = query + "SELECT CAST(SCOPE_IDENTITY() AS INT)";

            var rowEffected = await connection.ExecuteAsync(query, itemParams);

            return rowEffected;
        }

    }
}
