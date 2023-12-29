using CompanyManagementSystem.Core.Entities.Base;
using CompanyManagementSystem.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyManagementSystem.Core.Entities;

[Table("Request")]
public class Request : BaseEntity
{
    [Required]
    public RequestType RequestType { get; set; }

    public DateOnly? TimeOffStartDate { get; set; }

    public DateOnly? TimeOffEndDate { get; set; }

    public bool Accepted { get; set; } = false;

    #region Relations
    [Required]
    public int CreatorId { get; set; }
    public User Creator { get; set; }
    #endregion
}
