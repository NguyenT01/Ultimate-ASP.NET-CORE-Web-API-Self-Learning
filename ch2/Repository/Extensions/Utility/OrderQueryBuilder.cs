

using Entities.Models;
using System.Reflection;
using System.Text;

namespace Repository.Extensions.Utility;

public static class OrderQueryBuilder
{
    public static string CreateOrderQuery<T>(string orderByQueryString)
    {
        // phân tách OrderBy bởi dấu , [ví dụ: "name,age desc" thành ["name","age desc"]
        var orderParams = orderByQueryString.Trim().Split(',');

        // lấy các biến Instance và quyền truy cập Public trong <Model>
        var propertyInfos = typeof(Employee).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        // Tạo 1 QueryBuilder
        var orderQueryBuilder = new StringBuilder();

        // Lập từng param trong orderParams đó
        foreach (var param in orderParams)
        {
            // Nếu param là null
            if (string.IsNullOrWhiteSpace(param))
                continue;

            // Lấy phần đầu của param (nếu nó nằm ở cuối ví dụ như ["name desc"] thì thành ["name"])
            var propertyFromQueryName = param.Split(" ")[0];

            // So sánh tên biến của <Model> có tên nào giống với tên param không.
            // InvariantCultureIgnoreCase dùng để bỏ qua việc chữ hoa chữ thường.
            var objectProperty = propertyInfos.FirstOrDefault(
                pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

            // Nếu không có tên biến của <Model> phù hợp với param
            if (objectProperty == null)
                continue;

            // Nếu tên string cuối của param nếu không để hoặc không ghi desc thì mặc định là asc
            // ["age" => "age ascending"] ["name desc" => "name ascending"]
            var direction = param.EndsWith(" desc") ? "descending" : "ascending";

            // Thêm string vời stringBuilder
            orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction},");
        }

        // String Builder loại bỏ các ký tự khoảng cách và phẩy ở đầu và cuối và chuyển về lại thành String thường
        // ["Name ascending,Age descending,"] => ["Name ascending,Age descending"]
        var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

        return orderQuery;
    }
}
