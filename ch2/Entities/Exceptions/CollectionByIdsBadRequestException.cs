

namespace Entities.Exceptions;

public sealed class CollectionByIdsBadRequestException : Exception
{
    public CollectionByIdsBadRequestException()
        : base("Collection count mismatch comparing to ids") { }
}
