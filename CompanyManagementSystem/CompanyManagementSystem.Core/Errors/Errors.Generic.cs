using ErrorOr;

namespace CompanyManagementSystem.Core.Errors;

public static partial class Errors
{
    public static class Generic
    {
        public static Error EntityNotFound(string message = null) => Error.NotFound(description: message);

        public static Error EntityIsNull(string message = null) => Error.Failure(description: message);
    }
}
