
using Contracts;
using Service.Contracts;

namespace Service;

internal sealed class EmployeeService : IEmployeeService
{
    private readonly IRepositoryManager _repository;
    private readonly IloggerManager _logger;
    public EmployeeService(IRepositoryManager repository, IloggerManager logger)
    {
        _repository = repository;
        _logger = logger;
    }
}
