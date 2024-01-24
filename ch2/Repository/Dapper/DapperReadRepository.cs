
using Contracts.Dapper;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Repository.Dapper;

public class DapperReadRepository : DapperContext, IDapperReadRepository
{
    public DapperReadRepository(IConfiguration config) : base(config) { }

    public async Task<IEnumerable<T>> GetAll<T>(string query)
    {
        using (var connection = CreateConnection())
        {
            return await connection.QueryAsync<T>(query);
        }
    }
    public async Task<T?> FindByConditionSingle<T>(string query, object condition)
    {
        using (var connection = CreateConnection())
        {
            return await connection.QuerySingleOrDefaultAsync<T>(query, condition);
        }
    }

    public async Task<IEnumerable<T>> FindByConditionList<T>(string query, object listCondition)
    {
        using (var connection = CreateConnection())
        {
            return await connection.QueryAsync<T>(query, listCondition);
        }
    }

    public async Task<T> FindByConditionSingleNotNull<T>(string query, object condition)
    {
        using (var connection = CreateConnection())
        {
            return await connection.QuerySingleAsync<T>(query, condition);
        }
    }

    //Use Count or Sum
    public async Task<dynamic?> ExecuteScalaAsync<T>(string query, object? condition)
    {
        using (var connection = CreateConnection())
        {
            return await connection.ExecuteScalarAsync<T>(query, condition);
        }
    }
}
