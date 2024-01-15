﻿using Contracts;
using Entities.Models;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext context) : base(context) { }

        public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges)
            => FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
                .OrderBy(e => e.Name).ToList();

        public Employee GetEmployee(Guid companyId, Guid id, bool trackChanges)
            => FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id), trackChanges)
                .SingleOrDefault()!;

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Add(employee);
        }
        public void DeleteEmployee(Employee employee)
            => Delete(employee);
    }
}
