using CompanyManagementSystem.Core.DTOs.Base;
using CompanyManagementSystem.Core.Entities;

namespace CompanyManagementSystem.Core.DTOs;

public class RequestDTO : BaseDTO
{
    public int RequestType { get; set; }

    public DateOnly? TimeOffStartDate { get; set; }

    public DateOnly? TimeOffEndDate { get; set; }

    public bool Accepted { get; set; } = false;

    #region Relations
    public int CreatorId { get; set; }

    public UserDTO Creator { get; set; }
    #endregion
}
