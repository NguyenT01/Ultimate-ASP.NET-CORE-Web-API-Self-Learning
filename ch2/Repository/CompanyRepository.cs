using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges)
            => await FindAll(trackChanges)
                .OrderBy(comp => comp.Name)
                .ToListAsync();

        public async Task<Company> GetCompanyAsync (Guid companyId, bool trackChanges)
            => await FindByCondition(c => c.Id.Equals(companyId), trackChanges)
                    .SingleOrDefaultAsync()!;

        public void CreateCompany(Company company)
            => Add(company);

        public async Task<IEnumerable<Company>> GetByIdsAsync (IEnumerable<Guid> ids, bool trackChanges)
            => await FindByCondition(x => ids.Contains(x.Id), trackChanges)
                .ToListAsync();
        public void DeleteCompany(Company company)
            => Delete(company);

    }
}


