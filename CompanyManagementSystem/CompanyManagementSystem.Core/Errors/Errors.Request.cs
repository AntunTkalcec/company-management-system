using ErrorOr;

namespace CompanyManagementSystem.Core.Errors;

public static partial class Errors
{
    public static class Request
    {
        public static Error RequestNotFound(string message = null) => Error.NotFound(description: message);
    }
}
