

namespace Contracts.Dapper;

public interface IDapperManager
{
    IDapperReadRepository Read { get; }
    IDapperCreateRepository Create { get; }
    IDapperDeleteRepository Delete { get; }
}
