using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
    }
}
