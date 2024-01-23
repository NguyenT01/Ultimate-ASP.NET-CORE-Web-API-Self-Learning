using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        public Task<IEnumerable<CompanyDto1>> GetAllCompaniesAsync(bool trackChanges);
        public Task<CompanyDto1> GetCompanyAsync(Guid companyId, bool trackChanges);
        public Task<CompanyDto1> CreateCompanyAsync(CompanyForCreationDto company);
        public Task<IEnumerable<CompanyDto1>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        public Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollection
            (IEnumerable<CompanyForCreationDto> companyCollection);
        public Task DeleteCompanyAsync(Guid companyId, bool trackChanges);
        public Task UpdateCompanyAsync(Guid companyId, CompanyForUpdateDto companyForUpdate, bool trackChanges);
    }
}
