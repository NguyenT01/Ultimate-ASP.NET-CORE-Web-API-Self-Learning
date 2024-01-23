

namespace Shared.DataTransferObjects;

public record EmployeeDto(Guid Id, string Name, int Age, string Position);

public record EmployeeDto1(Guid EmployeeId, string Name, int Age, string Position, Guid CompanyId);
