using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;
using System.Linq;
using Repository.Extensions;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext context) : base(context) { }

        public async Task<PageList<Employee>> GetEmployeesAsync(Guid companyId, 
            EmployeeParameters employeeParameters, bool trackChanges)
        {
            var employees = await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
                .FilterEmployees(employeeParameters.MinAge, employeeParameters.MaxAge)
                .Search(employeeParameters.SearchTerm)
                .Sort(employeeParameters.OrderBy)
                .ToListAsync();

            return PageList<Employee>.ToPagedList(employees, employeeParameters.PageNumber,
                employeeParameters.PageSize);
        }
        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges)
            => await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id), trackChanges)
                .SingleOrDefaultAsync()!;

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Add(employee);
        }
        public void DeleteEmployee(Employee employee)
            => Delete(employee);
    }
}
