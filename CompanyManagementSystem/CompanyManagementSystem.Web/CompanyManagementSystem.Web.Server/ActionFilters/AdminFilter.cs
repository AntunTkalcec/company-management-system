using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace CompanyManagementSystem.Web.Server.ActionFilters;

public class AdminFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        ClaimsPrincipal httpUser = context.HttpContext.User;

        Claim? isAdmin = httpUser.Claims.SingleOrDefault(_ => _.Type == "IsAdmin");

        if (isAdmin is null)
            context.Result = new ForbidResult();

        if (isAdmin?.Value != string.Empty)
        {
            if (isAdmin?.Value is "True")
                base.OnActionExecuting(context);
            else
                context.Result = new ForbidResult();
        }
        else context.Result = new ForbidResult();
    }
}
