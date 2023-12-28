using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace CompanyManagementSystem.Web.Server.ActionFilters;

public class AuthorizationFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        ClaimsPrincipal httpUser = context.HttpContext.User;

        if (!httpUser.Identity.IsAuthenticated) 
            context.Result = new ForbidResult();

        else base.OnActionExecuting(context);
    }
}
