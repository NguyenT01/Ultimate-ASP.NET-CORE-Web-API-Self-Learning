using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges);
        public EmployeeDto GetEmployee(Guid companyId, Guid id, bool trackChanges);
        public EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDto employeeForCreation,
            bool trackChanges);
        public void DeleteEmployee(Guid companyId, Guid id, bool trackChanges);
        public void UpdateEmployeeForCompany(Guid companyId, Guid id,
            EmployeeForUpdateDto employeeForUpdate, bool compTrackChanges, bool empTrackChanges);
    }
}
