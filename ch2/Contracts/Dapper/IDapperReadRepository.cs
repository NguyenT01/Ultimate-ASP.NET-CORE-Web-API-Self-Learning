

namespace Contracts.Dapper;

public interface IDapperReadRepository
{
    public Task<IEnumerable<T>> GetAll<T>(string query);
    public Task<T?> FindByConditionSingle<T>(string query, object condition);
    public Task<T> FindByConditionSingleNotNull<T>(string query, object condition);
    public Task<IEnumerable<T>> FindByConditionList<T>(string query, object listCondition);
    public Task<dynamic?> ExecuteScalaAsync<T>(string query, object? condition = null);
}
