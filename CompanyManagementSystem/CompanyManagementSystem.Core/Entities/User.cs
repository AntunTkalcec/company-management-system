using CompanyManagementSystem.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyManagementSystem.Core.Entities;

[Table("User")]
public class User : BaseEntity
{
    [Required]
    [StringLength(50)]
    public string UserName { get; set; }

    [Required]
    [StringLength(50)]
    public string Password { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    [Required]
    [StringLength(50)]
    public string Email { get; set; }

    [Required]
    public bool IsAdmin { get; set; }

    public int? TimeOffCount { get; set; }

    public double? Salary { get; set; }

    [Required]
    public bool IsOnLeave { get; set; } = false;

    #region Relations
    public int? CompanyId { get; set; }
    public Company Company { get; set; }
    #endregion
}
