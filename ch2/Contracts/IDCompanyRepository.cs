

using Entities.Models;

namespace Contracts;

public interface IDCompanyRepository
{
    public Task<IEnumerable<Company>> DGetCompanies();
    public Task<Company> DGetCompany(Guid id);
}
