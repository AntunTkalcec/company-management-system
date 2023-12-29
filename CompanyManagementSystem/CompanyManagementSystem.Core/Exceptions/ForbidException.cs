namespace CompanyManagementSystem.Core.Exceptions;

public class ForbidException(string username) : Exception($"Access to that is forbidden for user {username}")
{
}
