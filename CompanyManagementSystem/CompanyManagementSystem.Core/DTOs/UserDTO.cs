using CompanyManagementSystem.Core.Authentication;
using CompanyManagementSystem.Core.DTOs.Base;
using CompanyManagementSystem.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace CompanyManagementSystem.Core.DTOs;

public class UserDTO : BaseDTO
{
    [Required(ErrorMessage = "Username is required")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "This is not a valid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Need 6-20 characters, 1 uppercase letter, 1 lowercase letter and 1 number.")]
    public string Password { get; set; }

    public AuthenticationInfo AuthenticationInfo { get; set; }

    public bool IsAdmin { get; set; }

    public int? TimeOffCount { get; set; }

    public double? Salary { get; set; }

    [Required]
    public bool IsOnLeave { get; set; }

    #region Relations
    public int? CompanyId { get; set; }
    public CompanyDTO Company { get; set; }

    public List<RequestDTO> Requests { get; set; }
    #endregion
}
