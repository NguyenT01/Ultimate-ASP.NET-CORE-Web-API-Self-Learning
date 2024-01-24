

namespace Contracts.Dapper;

public interface IDapperDeleteRepository
{
    public Task<int> DeleteItem(string query, object? condition);
}
