namespace CompanyManagementSystem.Core.Exceptions;

public class BadRequestException(string message) : Exception
{
    public override string Message => message;
}
