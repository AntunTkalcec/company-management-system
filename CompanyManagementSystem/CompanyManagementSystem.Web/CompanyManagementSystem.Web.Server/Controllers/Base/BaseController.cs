using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagementSystem.Web.Server.Controllers.Base;

public class BaseController : ControllerBase
{
    public int UserId
    {
        get
        {
            _ = int.TryParse(User.Claims.SingleOrDefault(t => t.Type == "UserId")?.Value, out int userId);
            return userId;
        }
    }

    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0)
        {
            return Problem();
        }

        if (errors.TrueForAll(error => error.Type is ErrorType.Validation))
        {
            return Problem(); // todo handle validation 
        }

        return Problem(errors.FirstOrDefault());
    }

    private ObjectResult Problem(Error error)
    {
        int statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: error.Description);
    }
}