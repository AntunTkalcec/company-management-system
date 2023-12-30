namespace CompanyManagementSystem.Core.Exceptions;

public class NotPartOfCompanyException(string message) : Exception
{
    public override string Message => message;
}
