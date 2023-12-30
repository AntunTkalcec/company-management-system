namespace CompanyManagementSystem.Core.DTOs.Input;

public class RequestInputModel
{
    public int RequestType { get; set; }

    public DateOnly? TimeOffStartDate { get; set; }

    public DateOnly? TimeOffEndDate { get; set; }

    public bool Accepted { get; set; } = false;

    public int CreatorId { get; set; }
}
