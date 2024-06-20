using ErrorOr;

namespace CompanyManagementSystem.Core.Errors;

public static partial class Errors
{
    public static class Company
    {
        public static Error CompanyNotFound(string message = null) => Error.NotFound(description: message);

        public static Error CompanyValidationFailed(string message = null) => Error.Validation(description: message);
    }
}
