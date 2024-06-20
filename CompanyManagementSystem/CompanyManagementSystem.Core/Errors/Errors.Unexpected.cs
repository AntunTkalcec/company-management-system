using ErrorOr;

namespace CompanyManagementSystem.Core.Errors;

public static partial class Errors
{
    public static class Unexpected
    {
        public static Error InternalServerError(string message = null)
            => Error.Unexpected("ERR_UNEXPECTED", message ?? "An unexpected error occurred.");
    }
}
