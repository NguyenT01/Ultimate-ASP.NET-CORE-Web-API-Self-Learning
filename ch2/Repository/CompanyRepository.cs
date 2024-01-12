using Contracts;
using Entities.Models;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext context) : base(context)
        {

        }
        public IEnumerable<Company> GetAllCompanies(bool trackChanges)
            => FindAll(trackChanges)
                .OrderBy(comp => comp.Name)
                .ToList();

        public Company GetCompany(Guid companyId, bool trackChanges)
            => FindByCondition(c => c.Id.Equals(companyId), trackChanges)
                    .SingleOrDefault()!;

        public void CreateCompany(Company company)
            => Add(company);

        public IEnumerable<Company> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
            => FindByCondition(x => ids.Contains(x.Id), trackChanges)
                .ToList();

    }
}
