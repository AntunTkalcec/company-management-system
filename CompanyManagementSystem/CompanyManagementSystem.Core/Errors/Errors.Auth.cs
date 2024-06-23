using ErrorOr;

namespace CompanyManagementSystem.Core.Errors;

public static partial class Errors
{
    public static class Auth
    {
        public static Error RefreshTokenInvalid(string message = null) => Error.Unauthorized(description: message);

        public static Error ClaimsFailure(string message = null) => Error.Failure(description: message);

        public static Error RefreshTokenFailure(string message = null) => Error.Failure(description: message);
    }
}
