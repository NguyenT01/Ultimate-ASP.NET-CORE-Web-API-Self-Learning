

namespace Shared.RequestFeatures;

public class EmployeeParameters : RequestParameters
{
    //CONSTRUCTOR
    public EmployeeParameters()
        => OrderBy = "name";

    public uint MinAge { get; set; }
    public uint MaxAge { get; set; } = int.MaxValue;
    public bool ValidaAgeRange => MaxAge > MinAge;
    public string? SearchTerm { get; set; }
}
