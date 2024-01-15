using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
        public CompanyDto GetCompany(Guid companyId, bool trackChanges);
        public CompanyDto CreateCompany(CompanyForCreationDto company);
        public IEnumerable<CompanyDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        public (IEnumerable<CompanyDto> companies, string ids) CreateCompanyCollection
            (IEnumerable<CompanyForCreationDto> companyCollection);
        public void DeleteCompany(Guid companyId, bool trackChanges);
        public void UpdateCompany(Guid companyId, CompanyForUpdateDto companyForUpdate, bool trackChanges);
    }
}
