using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        public Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(bool trackChanges);
        public Task<CompanyDto> GetCompanyAsync(Guid companyId, bool trackChanges);
        public Task<CompanyDto> CreateCompanyAsync(CompanyForCreationDto company);
        public Task<IEnumerable<CompanyDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        public Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollection
            (IEnumerable<CompanyForCreationDto> companyCollection);
        public Task DeleteCompanyAsync(Guid companyId, bool trackChanges);
        public Task UpdateCompanyAsync(Guid companyId, CompanyForUpdateDto companyForUpdate, bool trackChanges);
    }
}
