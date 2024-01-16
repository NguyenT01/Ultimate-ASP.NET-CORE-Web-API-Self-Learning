using System.ComponentModel.DataAnnotations;

namespace Entities.CustomValidator;

public class CountryValidator : ValidationAttribute
{
    public override bool IsValid(object? attribute)
    {
        if (attribute is null)
            return true;

        if(attribute is string ct)
        {
            string[] exceptCountries = { "TR", "CR", "XC", "NU", "PA" };
            return !exceptCountries.Contains(ct);
        }

        return false;
    }
}