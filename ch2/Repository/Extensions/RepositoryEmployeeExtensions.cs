
using System.Linq.Dynamic.Core;
using Entities.Models;
using System.Reflection;
using System.Text;
using Repository.Extensions.Utility;

namespace Repository.Extensions;

public static class RepositoryEmployeeExtensions
{
    public static IQueryable<Employee> FilterEmployees(this IQueryable<Employee> employees,
        uint minAge, uint maxAge)
            => employees.Where(e => (e.Age >= minAge && e.Age <= maxAge));
    public static IQueryable<Employee> Search(this IQueryable<Employee> employees, string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return employees;

        var lowerCaseTerm = searchTerm.Trim().ToLower();

        return employees.Where(e => e.Name!.ToLower().Contains(lowerCaseTerm));
    }

    public static IQueryable<Employee> Sort(this IQueryable<Employee> employees, string? orderByQueryString)
    {
        // Kiểm tra QueryString OrderBy có null hay để trống hay không
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return employees.OrderBy(e => e.Name);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Employee>(orderByQueryString);

        // Nếu query đó rỗng
        if (string.IsNullOrWhiteSpace(orderQuery))
            return employees.OrderBy(e => e.Name);

        // Bình thường thì query với điều kiện đã được thực hiện
        return employees.OrderBy(orderQuery);
    }
}
