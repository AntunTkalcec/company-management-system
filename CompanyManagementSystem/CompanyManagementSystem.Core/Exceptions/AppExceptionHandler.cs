using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CompanyManagementSystem.Core.Exceptions;

public class AppExceptionHandler(ILogger<AppExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        (int statusCode, string message) = exception switch
        {
            ForbidException => ((int)HttpStatusCode.Forbidden, "You do not have access to that."),
            BadRequestException badRequestException => ((int)HttpStatusCode.BadRequest, badRequestException.Message),
            NotFoundException notFoundException => ((int)HttpStatusCode.NotFound, notFoundException.Message),
            _ => ((int)HttpStatusCode.InternalServerError, "Something went wrong.")
        };

        logger.LogError("An error occurred: {msg}", exception.Message);

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(message, cancellationToken);

        return true;
    }
}
