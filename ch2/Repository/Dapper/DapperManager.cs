

using Contracts.Dapper;
using Microsoft.Extensions.Configuration;

namespace Repository.Dapper;

public class DapperManager : IDapperManager
{
    private readonly IDapperReadRepository _dapperRead;
    private readonly IDapperCreateRepository _dapperCreate;

    public DapperManager(IConfiguration config)
    {
        _dapperRead = new DapperReadRepository(config);
        _dapperCreate = new DapperCreateRepository(config);
    }

    public IDapperReadRepository Read => _dapperRead;
    public IDapperCreateRepository Create => _dapperCreate;
}
