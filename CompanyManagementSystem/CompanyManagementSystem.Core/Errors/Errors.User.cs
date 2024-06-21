using ErrorOr;

namespace CompanyManagementSystem.Core.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error UserNotFound(string message = null) => Error.NotFound(description: message);

        public static Error UserNotPartOfCompany(string message = null) => Error.Failure(description: message);

        public static Error UserValidationFailed(string message = null) => Error.Validation(description: message);
    }
}
