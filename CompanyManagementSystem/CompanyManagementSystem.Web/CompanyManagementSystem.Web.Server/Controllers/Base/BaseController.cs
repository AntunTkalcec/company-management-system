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
}