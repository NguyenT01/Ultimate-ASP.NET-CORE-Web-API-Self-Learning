using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Dynamic;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        public Task<(IEnumerable<ExpandoObject> employees, MetaData metaData)> GetEmployeesAsync(Guid companyId, 
            EmployeeParameters employeeParameters, bool trackChanges);
        public Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges);
        public Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto employeeForCreation,
            bool trackChanges);
        public Task DeleteEmployeeAsync(Guid companyId, Guid id, bool trackChanges);
        public Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid id,
            EmployeeForUpdateDto employeeForUpdate, bool compTrackChanges, bool empTrackChanges);
    }
}
