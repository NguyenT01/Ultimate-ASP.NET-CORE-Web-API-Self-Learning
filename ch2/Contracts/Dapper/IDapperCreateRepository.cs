

namespace Contracts.Dapper;

public interface IDapperCreateRepository
{
    public Task<int> CreateItem<T>(string query, object itemParams);
}
