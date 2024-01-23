namespace Shared.DataTransferObjects;


public record CompanyDto
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? FullAddress { get; init; }
}

public record CompanyDto1
{
    public Guid CompanyId { get; init; }

    public string? Name { get; init; }

    public string? Address { get; init; }

    public string? Country { get; init; }
}
