
using Shared.RequestFeatures;
using Entities.Models;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        Task<PageList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges);
        Task<Employee> GetEmployeeAsync(Guid comapanyId, Guid id, bool trackChanges);
        void CreateEmployeeForCompany(Guid companyId, Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
