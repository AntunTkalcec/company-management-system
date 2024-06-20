using ErrorOr;

namespace CompanyManagementSystem.Core.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error UserNotFound(string message = null) => Error.NotFound(description: message);
    }
}
