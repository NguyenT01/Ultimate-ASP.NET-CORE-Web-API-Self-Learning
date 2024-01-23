

namespace Contracts.Dapper;

public interface IDapperManager
{
    IDapperReadRepository Read { get; }
    IDapperCreateRepository Create { get; }
}
