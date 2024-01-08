
using Contracts;
using Service.Contracts;

namespace Service;
internal sealed class CompanyService : ICompanyService
{
    private readonly IRepositoryManager _repository;
    private readonly IloggerManager _logger;

    public CompanyService(IRepositoryManager repository, IloggerManager logger)
    {
        _repository = repository;
        _logger = logger;
    }
}